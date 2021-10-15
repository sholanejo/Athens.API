using System;
using AthensLibrary.Data.Implementations;
using AthensLibrary.Data.Interface;
using AthensLibrary.Model.Entities;
using AthensLibrary.Model.Enumerators;
using AthensLibrary.Service.Implementations;
using AthensLibrary.Service.Interface;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AthensLibrary.Extensions
{
    public static class MiddleWareExtension
    {  
        public static void RegisterServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AthensDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("AthensConnection"),
                    b => b.MigrationsAssembly("AthensLibrary"));
            });

            services.AddIdentity<User, Role>(options =>
            {
                options.User.RequireUniqueEmail = true;
                options.Password.RequireDigit = true;
                options.Password.RequiredLength = 8;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireUppercase = true;
                options.Password.RequiredUniqueChars = 1;
                options.Lockout.MaxFailedAccessAttempts = 4;
                options.Lockout.AllowedForNewUsers = true;
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(10);
            }).AddEntityFrameworkStores<AthensDbContext>().AddDefaultTokenProviders();

            services.AddAuthorization(option => option.AddPolicy("AdminRolePolicy", p => p.RequireRole(Roles.Admin.ToString())));
            services.AddAuthorization(option => option.AddPolicy("LibraryUserRolePolicy", p => p.RequireRole(Roles.LibraryUser.ToString())));
            services.AddAuthorization(option => option.AddPolicy("AuthorRolePolicy", p => p.RequireRole(Roles.Author.ToString())));
            services.AddTransient<DbContext, AthensDbContext>();
            services.AddTransient<IServiceFactory, ServiceFactory>();
            services.AddTransient<IAuthorService, AuthorService>();
            services.AddTransient<ILibraryUserService, LibraryUserService>();
            services.AddTransient<IBookService, BookService>();
            services.AddTransient<ICategoryService, CategoryService>();
            services.AddTransient<IUnitofWork, UnitofWork<AthensDbContext>>();
        }
    }
}
