using AthensLibrary.Model.Entities;
using AthensLibrary.Model.Enumerators;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace AthensLibrary.Configurations
{
    public static class SeedAuthor
    {
        public static async Task SeedAuthorAsync(UserManager<User> authorManager, RoleManager<Role> roleManager, AthensDbContext context)
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

            if (authorManager.Users.All(a => a.Id != author1.Id))
            {
                var user1 = await authorManager.FindByEmailAsync(author1.Email);
                var user2 = await authorManager.FindByEmailAsync(author2.Email);
                var user3 = await authorManager.FindByEmailAsync(author3.Email);

                if (user1 == null || user1 == null || user3 == null)
                {
                    await authorManager.CreateAsync(author1, "Shola-1234");
                    await authorManager.CreateAsync(author2, "Shola-1234");
                    await authorManager.CreateAsync(author3, "Shola-1234");

                    await authorManager.AddToRoleAsync(author1, Roles.Author.ToString());
                    await authorManager.AddToRoleAsync(author2, Roles.Author.ToString());
                    await authorManager.AddToRoleAsync(author3, Roles.Author.ToString());

                    if (context.Database.GetPendingMigrations().Any())
                    {
                        context.Database.Migrate();
                    }
                    if (!context.Authors.Any())
                    {
                        context.Authors.AddRange
                            (
                            new Author { Id = new Guid("8bb6b0fa-6611-4af3-84e5-a847e76e1ac3"), UserId = author1.Id, IsActive = true, IsDeleted = false },
                            new Author { Id = new Guid("7d4bc279-823a-4fe3-b62d-62568528c2f2"), UserId = author2.Id, IsActive = true, IsDeleted = false },
                            new Author { Id = new Guid("cb5a2153-6447-4195-824f-6f04cac88718"), UserId = author3.Id, IsActive = true, IsDeleted = false }
                            );
                        context.SaveChanges();
                    }
                }
            }
        }
    }
}
