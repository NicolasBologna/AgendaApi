using AgendaApi.Entities;
using AgendaApi.Models;
using AgendaApi.Repositories.Interfaces;
using AgendaApi.Services.Implementations;
using Moq;

namespace AgendaApi.Tests
{
    [TestFixture]
    public class ContactServiceTests
    {
        private Mock<IContactRepository> _mockRepository;
        private ContactService _service;

        [SetUp]
        public void Setup()
        {
            _mockRepository = new Mock<IContactRepository>();
            _service = new ContactService(_mockRepository.Object);
        }

        [Test]
        public void GetAllByUser_ShouldReturnListOfContacts()
        {
            // Arrange
            int userId = 1;
            var contacts = new List<Contact>
            {
                new Contact { Id = 1, FirstName = "John", LastName = "Doe", UserId = userId },
                new Contact { Id = 2, FirstName = "Jane", LastName = "Smith", UserId = userId }
            };

            _mockRepository.Setup(repo => repo.GetAllByUser(userId)).Returns(contacts);

            // Act
            var result = _service.GetAllByUser(userId);

            // Assert
            Assert.That(result.Count, Is.EqualTo(2));
            Assert.That(result[0].FirstName, Is.EqualTo("John"));
            Assert.That(result[1].FirstName, Is.EqualTo("Jane"));
        }

        [Test]
        public void GetOneByUser_ShouldReturnContact_WhenExists()
        {
            // Arrange
            int userId = 1;
            int contactId = 1;
            var contact = new Contact { Id = contactId, FirstName = "John", LastName = "Doe", UserId = userId };

            _mockRepository.Setup(repo => repo.GetOneByUser(userId, contactId)).Returns(contact);

            // Act
            var result = _service.GetOneByUser(userId, contactId);

            // Assert
            Assert.IsNotNull(result);
            Assert.That(result.Id, Is.EqualTo(contactId));
        }

        [Test]
        public void Create_ShouldCallRepositoryWithCorrectContact()
        {
            // Arrange
            int loggedUserId = 1;
            var dto = new CreateAndUpdateContactDto("John", "Doe", Email: "john@example.com");

            // Act
            _service.Create(dto, loggedUserId);

            // Assert
            _mockRepository.Verify(repo => repo.Create(It.Is<Contact>(c =>
                c.FirstName == dto.FirstName &&
                c.LastName == dto.LastName &&
                c.UserId == loggedUserId)), Times.Once);
        }

        [Test]
        public void Update_ShouldModifyExistingContact()
        {
            // Arrange
            int contactId = 1;
            var existingContact = new Contact { Id = contactId, FirstName = "OldName" };
            var dto = new CreateAndUpdateContactDto("NewName");

            _mockRepository.Setup(repo => repo.GetByContactId(contactId)).Returns(existingContact);

            // Act
            _service.Update(dto, contactId);

            // Assert
            _mockRepository.Verify(repo => repo.Update(It.Is<Contact>(c => c.FirstName == "NewName"), contactId), Times.Once);
        }

        [Test]
        public void Delete_ShouldCallRepositoryDeleteMethod()
        {
            // Arrange
            int contactId = 1;

            // Act
            _service.Delete(contactId);

            // Assert
            _mockRepository.Verify(repo => repo.Delete(contactId), Times.Once);
        }

        [Test]
        public void Export_ShouldReturnCSVString()
        {
            // Arrange
            int userId = 1;
            var contacts = new List<Contact>
            {
                new Contact { Id = 1, FirstName = "John", LastName = "Doe", UserId = userId },
                new Contact { Id = 2, FirstName = "Jane", LastName = "Smith", UserId = userId }
            };

            _mockRepository.Setup(repo => repo.GetAllByUser(userId)).Returns(contacts);

            // Act
            var result = _service.Export(userId);

            // Assert
            Assert.IsTrue(result.Contains("John"));
            Assert.IsTrue(result.Contains("Jane"));
        }

        [Test]
        public void ToggleFavorite_ShouldToggleIsFavoriteValue()
        {
            // Arrange
            int contactId = 1;
            var contact = new Contact { Id = contactId, IsFavorite = false };

            _mockRepository.Setup(repo => repo.GetByContactId(contactId)).Returns(contact);

            // Act
            var result = _service.ToggleFavorite(contactId);

            // Assert
            Assert.IsTrue(result);
            _mockRepository.Verify(repo => repo.Update(It.Is<Contact>(c => c.IsFavorite), contactId), Times.Once);
        }
    }
}