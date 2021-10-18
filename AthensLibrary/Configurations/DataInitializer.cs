using System;
using System.Linq;
using System.Threading.Tasks;
using AthensLibrary.Model.Entities;
using AthensLibrary.Model.Enumerators;
using Microsoft.AspNetCore.Identity;

namespace AthensLibrary.Configurations
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

        public static async Task SeedAuthorAsync(UserManager<User> authorManager, RoleManager<Role> roleManager)
        {
            var author1 = new User
            {
                UserName = "alexking@gmail.com",
                FullName = "King Alex",
                Email = "alexking@gmail.com",
                PhoneNumber = "07031204544",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true
            };

            var author2 = new User
            {
                UserName = "strategicsammy@gmail.com",
                FullName = "Strategic Sammy",
                Email = "sammystrategic@gmail.com",
                PhoneNumber = "07031204544",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true
            };

            var author3 = new User
            {
                UserName = "queenathena@gmail.com",
                FullName = "Queen Athena",
                Email = "queenathena@gmail.com",
                PhoneNumber = "07031204544",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true
            };

            await authorManager.CreateAsync(author1);
            await authorManager.CreateAsync(author2);
            await authorManager.CreateAsync(author3);
            await authorManager.AddToRoleAsync(author1, Roles.Author.ToString());
            await authorManager.AddToRoleAsync(author2, Roles.Author.ToString());
            await authorManager.AddToRoleAsync(author3, Roles.Author.ToString());

            new Author { UserId = author1.Id };
            new Author { UserId = author2.Id };
            new Author { UserId = author3.Id };
        }
    }
}
