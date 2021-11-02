using System;
using System.Linq;
using System.Threading.Tasks;
using AthensLibrary.Configurations;
using AthensLibrary.Extensions;
using AthensLibrary.Model.Entities;
using AthensLibrary.Model.Helpers.HelperClasses;
using AthensLibrary.Service.Interface;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace AthensLibrary
{
    public class Startup
    {      

        public Startup(IConfiguration configuration )
        {
            Configuration = configuration;            
        }
        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers(config =>
            {
                config.RespectBrowserAcceptHeader = true;
                config.ReturnHttpNotAcceptable = true;
            }).AddNewtonsoftJson()
            .AddXmlDataContractSerializerFormatters();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "AthensLibrary", Version = "v1" });
            });

            services.RegisterServices(Configuration);
            services.AddAutoMapper(typeof(MappingProfile));
            services.ConfigureJWT(Configuration);
            services.ConfigureSession();
            services.AddCustomMediaTypes();
        }
        public async void Configure(IApplicationBuilder app, IWebHostEnvironment env, 
            AthensDbContext dbContext, UserManager<User> userManager, RoleManager<Role> roleManager)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "AthensLibrary v1"));
            }
            app.ConfigureExceptionHandler();
            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseSession();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            await SeedRoleAdmin.seedRolesAdmin(dbContext, userManager, roleManager);
        }
    }
}
