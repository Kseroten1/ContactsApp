using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ContactsApp.Models
{
    public class ContactContext : IdentityDbContext<IdentityUser>
    {
        public ContactContext(DbContextOptions<ContactContext> options) : base(options)
        {
        }

        public DbSet<Contact> Contacts { get; set; } = null!;
        public DbSet<Category> Categories { get; set; } = null!;
        public DbSet<SubCategory> SubCategories { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasData(
              new Category { CategoryId = 1, Name = "Business" , AllowCustomSubCategory = false},
              new Category { CategoryId = 2, Name = "Private" },
              new Category { CategoryId = 3, Name = "Other" }
            );

            modelBuilder.Entity<SubCategory>().HasData(
                new SubCategory { SubCategoryId = 1, Name = "Boss", CategoryId = 1},
                new SubCategory { SubCategoryId = 2, Name = "Co-worker", CategoryId = 1 },
                new SubCategory { SubCategoryId = 3, Name = "Client", CategoryId = 1 }
                );

            modelBuilder.Entity<Contact>().HasIndex(c => c.Email).IsUnique();
            modelBuilder.Entity<Contact>().HasData(
              new Contact
              {
                  ContactId = 1,
                  FirstName = "Scott",
                  LastName = "Gudmestad",
                  PhoneNumber = "123-456-7890",
                  Email = "sgudmestad@gmail.com",
                  CategoryId = 1,
                  SubCategoryId = 2,
                  DateOfBirth = new DateOnly(2000, 12, 25),
              },
              new Contact
              {
                  ContactId = 2,
                  FirstName = "Gerry",
                  LastName = "Gudmestad",
                  PhoneNumber = "123-456-7890",
                  Email = "ggudmestad@gmail.com",
                  CategoryId = 1,
                  SubCategoryId = 2,
                  DateOfBirth = new DateOnly(1980, 1, 23),
              },
              new Contact
              {
                  ContactId = 3,
                  FirstName = "Blake",
                  LastName = "Boyer",
                  PhoneNumber = "123-456-7890",
                  Email = "bboyer@gmail.com",
                  CategoryId = 2,
                  DateOfBirth = new DateOnly(1983, 2, 3),
              }
         );
            base.OnModelCreating(modelBuilder);
        }
    }

}
