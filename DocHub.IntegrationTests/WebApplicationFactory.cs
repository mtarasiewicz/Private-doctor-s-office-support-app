using DocHub.Infrastructure.DbContext;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace DocHub.IntegrationTests;

public class WebApplicationFactory : WebApplicationFactory<Program>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        base.ConfigureWebHost(builder);
        builder.UseEnvironment("Test");
        builder.ConfigureServices(services =>
        {
            var descriptor = services.SingleOrDefault(service =>
                service.ServiceType == typeof(DbContextOptions<ApplicationDbContext>));
            if (descriptor is not null)
            {
                services.Remove(descriptor);
            }
            
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseInMemoryDatabase("DatabaseForTesting");
            });
        });
    }
}