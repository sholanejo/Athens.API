using System;
using System.Linq;
using System.Text;
using AthensLibrary.Data.Implementations;
using AthensLibrary.Data.Interface;
using AthensLibrary.Filters.ActionFilters;
using AthensLibrary.Model.Entities;
using AthensLibrary.Model.Enumerators;
using AthensLibrary.Service.Implementations;
using AthensLibrary.Service.Interface;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

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
            services.AddTransient<IServiceFactory, ServiceFactory>();
            services.AddTransient<DbContext, AthensDbContext>();
            services.AddTransient<IAuthorService, AuthorService>();
            services.AddTransient<ILibraryUserService, LibraryUserService>();
            services.AddTransient<IBookService, BookService>();
            services.AddTransient<ICategoryService, CategoryService>();
            services.AddTransient<IUnitOfWork, UnitofWork<AthensDbContext>>();
            services.AddScoped<IAuthentication, AuthenticationManager>();
            services.AddTransient<IUserService, UserService>();
            services.AddScoped<ValidationFilterAttribute>();
        }
        public static void ConfigureJWT(this IServiceCollection services, IConfiguration configuration)
        {
            var jwtSettings = configuration.GetSection("JwtSettings");
            var secretKey = configuration["SecretKey"];

            services.AddAuthentication(opt => { opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme; opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme; }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,

                    ValidIssuer = jwtSettings.GetSection("validIssuer").Value,
                    ValidAudience = jwtSettings.GetSection("validAudience").Value,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey))
                };
            });
        }
        public static void ConfigureSession(this IServiceCollection services)
        {
            services.AddDistributedMemoryCache();
            services.AddSession(Options =>
            {
                Options.IdleTimeout = TimeSpan.FromMinutes(5);
                Options.Cookie.HttpOnly = true;
                Options.Cookie.IsEssential = true;
            });
        }

        public static void AddCustomMediaTypes(this IServiceCollection services)
        {
            services.Configure<MvcOptions>(config =>
            {
                var newtonsoftJsonOutputFormatter = config.OutputFormatters.OfType<NewtonsoftJsonOutputFormatter>()?.FirstOrDefault();

                if (newtonsoftJsonOutputFormatter != null)
                {
                    newtonsoftJsonOutputFormatter.SupportedMediaTypes.Add("application/vnd.codemaze.hateoas+json");
                    newtonsoftJsonOutputFormatter.SupportedMediaTypes.Add("application/vnd.codemaze.apiroot+json");
                }
                var xmlOutputFormatter = config.OutputFormatters.OfType<XmlDataContractSerializerOutputFormatter>()?.FirstOrDefault();

                if (xmlOutputFormatter != null)
                {
                    xmlOutputFormatter.SupportedMediaTypes.Add("application/vnd.codemaze.hateoas+xml");
                    xmlOutputFormatter.SupportedMediaTypes.Add("application/vnd.codemaze.apiroot+xml");
                }

            });
        }
    }
}
