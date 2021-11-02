using AthensLibrary.Model.Entities;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;

namespace AthensLibrary.Test.Utilities
{
    public class CustomApplicationFactory<TStartup> : WebApplicationFactory<Startup>
    {
          HttpClient TestClient = null;
        protected override IHostBuilder CreateHostBuilder()
        {
            return Host.CreateDefaultBuilder()
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
        }


        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureTestServices(services =>
            {
                var serviceProvider = new ServiceCollection()
                .AddEntityFrameworkInMemoryDatabase()
                .BuildServiceProvider();

                ServiceDescriptor applicationDbRegister = services.SingleOrDefault(d => 
                d.ServiceType == typeof(DbContextOptions<AthensDbContext>));
                
                ServiceDescriptor applicationAuthRegister = services.SingleOrDefault(d => 
                d.ServiceType == typeof(AuthenticationOptions));

                services.Remove(applicationDbRegister);
                services.Remove(applicationAuthRegister);

                services.AddDbContext<AthensDbContext>(optionsAction =>
                {
                    optionsAction.UseInMemoryDatabase("AthensLibraryDb");
                });

            });

            builder.Configure(app =>
            {
                var dbContext = app.ApplicationServices.GetRequiredService<AthensDbContext>();
              
                SeedData.SeedinitialData(dbContext);

            });
        }


        public HttpClient GetTestClient()
        {
            TestClient = CreateClient();
            TestClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            base.ConfigureClient(TestClient);

            return TestClient;
        }
    }
}
