﻿using System;
using System.Linq;
using System.Threading.Tasks;
using AthensLibrary.Model.Entities;
using AthensLibrary.Model.Enumerators;
using AthensLibrary.Model.Helpers.HelperClasses;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
        public static async Task SeedAuthorAsync(this ModelBuilder builder, UserManager<User> userManager, RoleManager<Role> roleManager)
        {
            var defaultUser = new User
            {
                UserName = "briantracy@gmail.com",
                Email = "briantracy@gmail.com",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
                FullName = "Brian Tracy",
                PhoneNumber = "0817-926-5533",
            };
            if (userManager.Users.All(u => u.Id != defaultUser.Id))
            {
                var user = await userManager.FindByEmailAsync(defaultUser.Email);
                if (user == null)
                {
                    await userManager.CreateAsync(defaultUser, "Shola-1234");
                    await userManager.AddToRoleAsync(defaultUser, Roles.Author.ToString());
                    var auth = new Author
                    {
                        Id = new Guid("4d9436fd-8434-4121-a71a-867c549e0253"),
                        IsActive = true,
                        BorrowerId = RandomItemGenerators.GenerateBorrowerId(),
                        IsDeleted = false,
                        UserId = user.Id
                    };
                    builder.Entity<Author>().HasData(auth);
                }
            }
        }

        public static void SeedCategoryAsync(this ModelBuilder builder)
        {
            var cate1 = new Model.Entities.Category()
            {
                Id = new Guid("0d4bb327-727b-4863-b690-a569b6af59eb"),
                CategoryName = "Inspiration",
                CreatedAt = DateTime.Now
            };
            builder.Entity<Model.Entities.Category>().HasData(cate1);
        }

        public static void SeedBooksAsync(this ModelBuilder builder)
        {
            var book1 = new Book
            {
                ID = new Guid("7903fba2-e5d9-4d8b-9de3-319c2a37a865"),
                AuthorId = new Guid("4d9436fd-8434-4121-a71a-867c549e0253"),
                CreatedAt = DateTime.Now,
                Title = "Power of the mind",
                CategoryName = "Inspiration",
                InitialBookCount = 500,
                CurrentBookCount = 500,
                IsDeleted = false,
                PublicationYear = DateTime.Today.Subtract(new TimeSpan(10)),
            };
            var book2 = new Book
            {
                ID = new Guid("e670a86c-2019-423d-8abc-e59ea1ac05b6"),
                AuthorId = new Guid("4d9436fd-8434-4121-a71a-867c549e0253"),
                CreatedAt = DateTime.Now,
                Title = "Power of the mind",
                CategoryName = "Inspiration",
                InitialBookCount = 500,
                CurrentBookCount = 500,
                IsDeleted = false,
                PublicationYear = DateTime.Today.Subtract(new TimeSpan(10)),
            };
            builder.Entity<Book>().HasData(book1, book2);
        }
    }

}
