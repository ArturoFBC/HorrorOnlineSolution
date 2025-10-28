using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using HorrorOnline.Infrastructure.DbContext;

namespace IntegrationTests
{
    public class CustomWebApplicationFactory : WebApplicationFactory<Program>
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            base.ConfigureWebHost(builder);

            builder.UseEnvironment("Test");

            builder.ConfigureServices((Action<IServiceCollection>)(services =>
            {
                var descriptor = services.SingleOrDefault((Func<ServiceDescriptor, bool>)(temp => temp.ServiceType == typeof(DbContextOptions<ApplicationDbContext>)));

                if (descriptor != null)
                {
                    services.Remove(descriptor);
                }

                var descriptor2 = services.SingleOrDefault((Func<ServiceDescriptor, bool>)(temp => temp.ServiceType == typeof(DbContextOptions<ApplicationDbContext>)));

                EntityFrameworkServiceCollectionExtensions.AddDbContext<ApplicationDbContext>(services, (Action<DbContextOptionsBuilder>?)(options => options.UseInMemoryDatabase("DatabaseForTesting")));
                //ReplaceDataBaseContextForInMemoryMock(services);
            }));
        }
    }
