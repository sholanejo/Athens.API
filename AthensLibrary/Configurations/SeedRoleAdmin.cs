using System.Linq;
using System.Threading.Tasks;
using AthensLibrary.Model.Entities;
using AthensLibrary.Model.Helpers.HelperClasses;
using AthensLibrary.Service.Interface;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace AthensLibrary.Configurations
{
    public static class SeedRoleAdmin
    {
        //private readonly IServiceFactory serviceFactory;

        //public SeedRoleAdmin(IServiceFactory serviceFactory)
        //{
        //    this.serviceFactory = serviceFactory;
        //}

        public static async Task seedRolesAdmin(AthensDbContext context,
            UserManager<User> userManager, RoleManager<Role> roleManager)
        {
            if(context.Database.ProviderName != "Microsoft.EntityFrameworkCore.InMemory")
            {
                context.Database.Migrate();
            }

            //if (context.Set<Role>().ToList().Count == 0 || context.Set<User>().ToList().Count == 0)
            //{
            //    await DataInitializer.SeedRolesAsync(roleManager);
            //    await DataInitializer.SeedAdminAsync(userManager, roleManager);
            //}
            //if (context.Set<Author>().ToList().Count == 0)
            //{
            //    await Seed.SeedAuthorAsync(userManager, roleManager, context);
            //}
        }
    }
}
