using AgendaApi.Entities;
using Microsoft.EntityFrameworkCore;

namespace AgendaApi.Data
{
    public class AgendaContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Group> Groups { get; set; }

        public AgendaContext(DbContextOptions<AgendaContext> options) : base(options) //Acá estamos llamando al constructor de DbContext que es el que acepta las opciones
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            User karen = new User()
            {
                Id = 1,
                FirstName = "Karen",
                LastName = "Lasot",
                Password = "Pa$$w0rd",
                UserName = "karenbailapiola@gmail.com",
                Role = Models.Enum.Role.Admin,
            };
            User luis = new User()
            {
                Id = 2,
                FirstName = "Luis Gonzalez",
                LastName = "Gonzales",
                Password = "lamismadesiempre",
                UserName = "elluismidetotoras@gmail.com",
            };

            Contact jaimitoC = new Contact()
            {
                Id = 1,
                FirstName = "Jaimito",
                Company = "PwC",
                LastName = "Perez",
                Email = "jpreze@pwc.com",
                Number = "341457896",
                Description = "Plomero",
                UserId = karen.Id,
            };

            Contact pepeC = new Contact()
            {
                Id = 2,
                FirstName = "Pepe",
                Company = "Austral",
                LastName = "Ramirez",
                Email = "pramirez@austral.com",
                Number = "34156978",
                Description = "Papa",
                UserId = luis.Id,
            };

            Contact mariaC = new Contact()
            {
                Id = 3,
                FirstName = "Maria",
                Company = "google",
                LastName = "paez",
                Email = "mpaez@google.com",
                Number = "341457896",
                Description = "Jefa",
                UserId = karen.Id,
            };

            modelBuilder.Entity<User>().HasData(
                karen, luis);

            modelBuilder.Entity<Contact>().HasData(
                 jaimitoC, pepeC, mariaC
                 );

            modelBuilder.Entity<User>()
              .HasMany<Contact>(u => u.Contacts)
              .WithOne(c => c.User);

            modelBuilder.Entity<Contact>().
                HasMany<Group>().WithMany(c => c.Contacts);

            base.OnModelCreating(modelBuilder);
        }
    }
}
