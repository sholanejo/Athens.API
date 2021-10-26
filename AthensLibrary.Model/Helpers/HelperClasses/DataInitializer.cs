using System;
using System.Linq;
using System.Threading.Tasks;
using AthensLibrary.Model.Entities;
using AthensLibrary.Model.Enumerators;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace AthensLibrary.Model.Helpers.HelperClasses
{
    public static class DataInitializer
    {

        public static async Task SeedRolesAsync(RoleManager<Role> roleManager)
        {
            await roleManager.CreateAsync(new Role { Name = Roles.LibraryUser.ToString(), CreatedAt = DateTime.Now, CreatedBy = "Shola Nejo" });
            await roleManager.CreateAsync(new Role { Name = Roles.Admin.ToString(), CreatedAt = DateTime.Now, CreatedBy = "Shola Nejo" });
            await roleManager.CreateAsync(new Role { Name = Roles.Author.ToString(), CreatedAt = DateTime.Now, CreatedBy = "Shola Nejo" });
        }

        public static async Task SeedAdminAsync(UserManager<User> userManager, RoleManager<Role> roleManager)
        {
            var defaultUser = new User
            {
                UserName = "sholanejo@gmail.com",
                Email = "sholanejo@gmail.com",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
                FullName = "Shola Nejo",
                PhoneNumber = "0817-926-5533",
            };
            if (userManager.Users.All(u => u.Id != defaultUser.Id))
            {
                var user = await userManager.FindByEmailAsync(defaultUser.Email);
                if (user == null)
                {
                    await userManager.CreateAsync(defaultUser, "Shola-1234");
                    await userManager.AddToRoleAsync(defaultUser, Roles.Admin.ToString());
                }
            }
        }

        public static void SeedCategory(this ModelBuilder builder)
        {
            var cate1 = new Model.Entities.Category()
            {
                Id = new Guid("0d4bb327-727b-4863-b690-a569b6af59eb"),
                CategoryName = "Inspiration",
                CreatedAt = DateTime.Now
            };
            var cate2 = new Model.Entities.Category()
            {
                Id = new Guid("0d4bb123-727b-4863-b690-a569b6af59eb"),
                CategoryName = "Information Technology",
                CreatedAt = DateTime.Now
            };
            var cate3 = new Model.Entities.Category()
            {
                Id = new Guid("0d4bb456-727b-4863-b690-a569b6af59eb"),
                CategoryName = "Current Affairs",
                CreatedAt = DateTime.Now
            };
            var cate4 = new Model.Entities.Category()
            {
                Id = new Guid("0d4bb789-727b-4863-b690-a569b6af59eb"),
                CategoryName = "Religion",
                CreatedAt = DateTime.Now
            };
            var cate5 = new Model.Entities.Category()
            {
                Id = new Guid("0d4bb327-123b-4863-b690-a569b6af59eb"),
                CategoryName = "Science",
                CreatedAt = DateTime.Now
            };
            var cate6 = new Model.Entities.Category()
            {
                Id = new Guid("0d4bb327-456b-4863-b690-a569b6af59eb"),
                CategoryName = "Mathematic",
                CreatedAt = DateTime.Now
            };
            var cate7 = new Model.Entities.Category()
            {
                Id = new Guid("0d4bb327-789b-4863-b690-a569b6af59eb"),
                CategoryName = "Bezao",
                CreatedAt = DateTime.Now
            };
            var cate8 = new Model.Entities.Category()
            {
                Id = new Guid("0d4bb327-727b-1233-b690-a569b6af59eb"),
                CategoryName = "History",
                CreatedAt = DateTime.Now
            };
            builder.Entity<Model.Entities.Category>().HasData(cate1, cate2, cate3, cate4, cate5, cate6, cate7, cate8);
        }

        public static void SeedBooks(this ModelBuilder builder)
        {
            var book1 = new Book
            {
                ID = new Guid("7903fba2-e5d9-4d8b-9de3-319c2a37a865"),
                AuthorId = new Guid("8bb6b0fa-6611-4af3-84e5-a847e76e1ac3"),
                CreatedAt = DateTime.Now,
                Title = "Power of the mind",
                CategoryName = "Inspiration",
                InitialBookCount = 500,
                CurrentBookCount = 500,
                IsDeleted = false,
                PublicationYear = new DateTime(1998, 12, 30),
            };
            var book2 = new Book
            {
                ID = new Guid("e670a86c-2019-423d-8abc-e59ea1ac05b6"),
                AuthorId = new Guid("8bb6b0fa-6611-4af3-84e5-a847e76e1ac3"),
                CreatedAt = DateTime.Now,
                Title = "Sammy the Dev",
                CategoryName = "Information Technology",
                InitialBookCount = 500,
                CurrentBookCount = 500,
                IsDeleted = false,
                PublicationYear = new DateTime(1998, 12, 30),
            };
            var book3 = new Book
            {
                ID = new Guid("5c835629-2056-4549-aaa5-ab35d01bee0d"),
                AuthorId = new Guid("8bb6b0fa-6611-4af3-84e5-a847e76e1ac3"),
                CreatedAt = DateTime.Now,
                Title = "Alex the Dev",
                CategoryName = "Religion",
                InitialBookCount = 500,
                CurrentBookCount = 500,
                IsDeleted = false,
                PublicationYear = new DateTime(1998, 12, 30),
            };
            var book4 = new Book
            {
                ID = new Guid("0ee79eed-6b39-4196-8ec3-61321db5dcba"),
                AuthorId = new Guid("4d9436fd-8434-4121-a71a-867c549e0253"),
                CreatedAt = DateTime.Now,
                Title = "Shola the Dev",
                CategoryName = "Current Affairs",
                InitialBookCount = 500,
                CurrentBookCount = 500,
                IsDeleted = false,
                PublicationYear = new DateTime(1998, 12, 30),
            };
            var book5 = new Book
            {
                ID = new Guid("460ac0c0-64ed-4b73-bfbe-fc546d2afe79"),
                AuthorId = new Guid("8bb6b0fa-6611-4af3-84e5-a847e76e1ac3"),
                CreatedAt = DateTime.Now,
                Title = "Gideon the Komrad",
                CategoryName = "Science",
                InitialBookCount = 500,
                CurrentBookCount = 500,
                IsDeleted = false,
                PublicationYear = DateTime.Today.Subtract(new TimeSpan(10)),
            };
            var book6 = new Book
            {
                ID = new Guid("274fe8d0-2ce6-400a-8d7a-55a1dc6ce3b5"),
                AuthorId = new Guid("4d9436fd-8434-4121-a71a-867c549e0253"),
                CreatedAt = DateTime.Now,
                Title = "KC my gee",
                CategoryName = "Mathematic",
                InitialBookCount = 500,
                CurrentBookCount = 500,
                IsDeleted = false,
                PublicationYear = DateTime.Today.Subtract(new TimeSpan(10)),
            };
            var book7 = new Book
            {
                ID = new Guid("863bb0e8-7940-481a-a0e5-c591b8b76998"),
                AuthorId = new Guid("4d9436fd-8434-4121-a71a-867c549e0253"),
                CreatedAt = DateTime.Now,
                Title = "Chinedu Senior Man",
                CategoryName = "Bezao",
                InitialBookCount = 500,
                CurrentBookCount = 500,
                IsDeleted = false,
                PublicationYear = DateTime.Today.Subtract(new TimeSpan(10)),
            };
            var book8 = new Book
            {
                ID = new Guid("e25b7422-d61b-46f4-a5a3-9227482594a5"),
                AuthorId = new Guid("4d9436fd-8434-4121-a71a-867c549e0253"),
                CreatedAt = DateTime.Now,
                Title = "Tochukwu the Writer",
                CategoryName = "History",
                InitialBookCount = 500,
                CurrentBookCount = 500,
                IsDeleted = false,
                PublicationYear = DateTime.Today.Subtract(new TimeSpan(10)),
            };
            builder.Entity<Book>().HasData(book1, book2, book3, book4, book5,book6,book7,book8);
        }
    }

    

       

}
