﻿using AgendaApi.Entities;
using AgendaApi.Models;
using AgendaApi.Models.Dtos;
using AgendaApi.Models.Enum;
using AgendaApi.Repositories.Interfaces;
using AgendaApi.Services.Implementations;
using Moq;

namespace AgendaApi.Tests
{
    [TestFixture]
    public class UserServiceTests
    {
        private Mock<IUserRepository> _mockRepository;
        private UserService _userService;

        [SetUp]
        public void Setup()
        {
            _mockRepository = new Mock<IUserRepository>();
            _userService = new UserService(_mockRepository.Object);
        }

        [Test]
        public void GetById_ShouldReturnUser_WhenUserExists()
        {
            // Arrange
            int userId = 1;
            var user = new User
            {
                Id = userId,
                FirstName = "John",
                LastName = "Doe",
                Email = "johndoe",
                State = State.Active,
            };

            _mockRepository.Setup(repo => repo.GetById(userId)).Returns(user);

            // Act
            var result = _userService.GetById(userId);

            // Assert
            Assert.IsNotNull(result);
            Assert.That(result.Id, Is.EqualTo(userId));
            Assert.That(result.FirstName, Is.EqualTo("John"));
        }

        [Test]
        public void GetById_ShouldReturnNull_WhenUserDoesNotExist()
        {
            // Arrange
            int userId = 1;
            _mockRepository.Setup(repo => repo.GetById(userId)).Returns((User)null);

            // Act
            var result = _userService.GetById(userId);

            // Assert
            Assert.IsNull(result);
        }

        [Test]
        public void ValidateUser_ShouldReturnUser_WhenCredentialsAreCorrect()
        {
            // Arrange
            var authRequest = new AuthenticationRequestDto("johndoe", "password123");

            var user = new User
            {
                Email = "johndoe",
                Password = "password123"
            };

            _mockRepository.Setup(repo => repo.ValidateUser(It.IsAny<AuthenticationRequestDto>())).Returns(user);

            // Act
            var result = _userService.ValidateUser(authRequest);

            // Assert
            Assert.IsNotNull(result);
            Assert.That(result.Email, Is.EqualTo(authRequest.Email));
        }

        [Test]
        public void ValidateUser_ShouldReturnNull_WhenCredentialsAreIncorrect()
        {
            // Arrange
            var authRequest = new AuthenticationRequestDto("johndoe", "wrongpassword");

            _mockRepository.Setup(repo => repo.ValidateUser(It.IsAny<AuthenticationRequestDto>())).Returns((User)null);

            // Act
            var result = _userService.ValidateUser(authRequest);

            // Assert
            Assert.IsNull(result);
        }

        [Test]
        public void GetAll_ShouldReturnListOfUsers()
        {
            // Arrange
            var users = new List<User>
            {
                new() { Id = 1, FirstName = "John", LastName = "Doe", Email = "johndoe", State = State.Active },
                new() { Id = 2, FirstName = "Jane", LastName = "Smith", Email = "janesmith", State = State.Archived }
            };

            _mockRepository.Setup(repo => repo.GetAll()).Returns(users);

            // Act
            var result = _userService.GetAll();

            // Assert
            Assert.That(result.Count(), Is.EqualTo(2));
            Assert.That(result.First().FirstName, Is.EqualTo("John"));
        }

        [Test]
        public void Create_ShouldReturnNewUserId()
        {
            // Arrange
            var dto = new CreateAndUpdateUserDto("John", "Doe", "password123", "johndoe@gmail.com");
            var expectedResult = new UserDto(1, "John", "Doe", "johndoe@gmail.com", State.Active);

            _mockRepository.Setup(repo => repo.Create(It.IsAny<User>())).Returns(1);

            // Act
            var result = _userService.Create(dto);

            // Assert
            Assert.That(result, Is.EqualTo(expectedResult));
        }

        [Test]
        public void Update_ShouldCallRepositoryUpdate()
        {
            // Arrange
            int userId = 1;
            var dto = new CreateAndUpdateUserDto("John", "Doe", "newpassword", "username");

            // Act
            _userService.Update(dto, userId);

            // Assert
            _mockRepository.Verify(repo => repo.Update(It.Is<User>(u => u.FirstName == dto.FirstName), userId), Times.Once);
        }

        [Test]
        public void RemoveUser_ShouldCallRepositoryRemoveUser()
        {
            // Arrange
            int userId = 1;

            // Act
            _userService.RemoveUser(userId);

            // Assert
            _mockRepository.Verify(repo => repo.RemoveUser(userId), Times.Once);
        }

        [Test]
        public void CheckIfUserExists_ShouldReturnTrue_WhenUserExists()
        {
            // Arrange
            int userId = 1;
            _mockRepository.Setup(repo => repo.CheckIfUserExists(userId)).Returns(true);

            // Act
            var result = _userService.CheckIfUserExists(userId);

            // Assert
            Assert.That(result, Is.True);
        }

        [Test]
        public void CheckIfUserExists_ShouldReturnFalse_WhenUserDoesNotExist()
        {
            // Arrange
            int userId = 1;
            _mockRepository.Setup(repo => repo.CheckIfUserExists(userId)).Returns(false);

            // Act
            var result = _userService.CheckIfUserExists(userId);

            // Assert
            Assert.That(result, Is.False);
        }
    }
}
