using AthensLibrary.Model.Entities;
using AthensLibrary.Model.Enumerators;
using AthensLibrary.Model.Helpers.HelperClasses;
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
                PhoneNumberConfirmed = true,
                IsActive = true,
                IsDeleted = false,
                BorrowerId = RandomItemGenerators.GenerateBorrowerId()
            };

            var author2 = new User
            {
                UserName = "strategicsammy@gmail.com",
                FullName = "Strategic Sammy",
                Email = "strategicsammy@gmail.com",
                PhoneNumber = "07031204544",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
                IsActive = true,
                IsDeleted = false,
                BorrowerId = RandomItemGenerators.GenerateBorrowerId()
            };

            var author3 = new User
            {
                UserName = "queenathena@gmail.com",
                FullName = "Queen Athena",
                Email = "queenathena@gmail.com",
                PhoneNumber = "07031204544",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
                IsActive = true,
                IsDeleted = false,
                BorrowerId = RandomItemGenerators.GenerateBorrowerId()
            };

            var author4 = new User
            {
                UserName = "elonmusk@tesla.com",
                FullName = "Elon Musk",
                Email = "elonmusk@tesla.com",
                PhoneNumber = "07031204544",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
                IsActive = true,
                IsDeleted = false,
                BorrowerId = RandomItemGenerators.GenerateBorrowerId()
            };

            if (authorManager.Users.All(a => a.Id != author1.Id))
            {
                var user1 = await authorManager.FindByEmailAsync(author1.Email);
                var user2 = await authorManager.FindByEmailAsync(author2.Email);
                var user3 = await authorManager.FindByEmailAsync(author3.Email);
                var user4 = await authorManager.FindByEmailAsync(author4.Email);

                if (user1 is null || user2 is null || user3 is null || user4 is null)
                {
                    await authorManager.CreateAsync(author1, "Shola-1234");
                    await authorManager.CreateAsync(author2, "Shola-1234");
                    await authorManager.CreateAsync(author3, "Shola-1234");
                    await authorManager.CreateAsync(author4, "Shola-1234");

                    await authorManager.AddToRoleAsync(author1, Roles.Author.ToString());
                    await authorManager.AddToRoleAsync(author2, Roles.Author.ToString());
                    await authorManager.AddToRoleAsync(author3, Roles.Author.ToString());
                    await authorManager.AddToRoleAsync(author4, Roles.Author.ToString());

                    if (context.Database.GetPendingMigrations().Any())
                    {
                        context.Database.Migrate();
                    }
                    if (!context.Authors.Any())
                    {
                        context.Authors.AddRange
                            (
                            new Author { Id = new Guid("8bb6b0fa-6611-4af3-84e5-a847e76e1ac3"), UserId = author1.Id,  IsDeleted = false },
                            new Author { Id = new Guid("7d4bc279-823a-4fe3-b62d-62568528c2f2"), UserId = author2.Id,  IsDeleted = false },
                            new Author { Id = new Guid("cb5a2153-6447-4195-824f-6f04cac88718"), UserId = author3.Id,  IsDeleted = false },
                            new Author { Id = new Guid("cb5a1234-1234-4195-824f-6f04cac88888"), UserId = author4.Id,  IsDeleted = false }
                            );
                        context.SaveChanges();
                    }
                }
            }
        }
    }
}
