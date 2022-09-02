using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using System;
using System.Linq;
using TaskManager.Infrastructure.Database;

namespace TaskManager.Infrastructure.Tests.InMemory;

internal class TaskWebApplicationFactory<TStartup> : WebApplicationFactory<Program> where TStartup : Program
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            var descriptor = services.SingleOrDefault(s => s.ServiceType == typeof(DbContextOptions<TaskDbContext>));
            services.Remove(descriptor);

            services.AddDbContext<TaskDbContext>(options => options.UseInMemoryDatabase("InMemory"));
            services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<TaskDbContext>();

            var sp = services.BuildServiceProvider();

            using (var scope = sp.CreateScope())
            {
                var service = scope.ServiceProvider;
                var _context = service.GetRequiredService<TaskDbContext>();
                var logger = service.GetRequiredService<ILogger<TaskWebApplicationFactory<TStartup>>>();

                _context.Database.EnsureCreated();

                //var seedUsers = new UserTestData();
                //seedUsers.CreateUsers(service);

                try
                {
                    DbHelpers.CreateData(_context);
                }
                catch (Exception ex)
                {
                    logger.LogError($"Error occured while seeding database: {ex.Message}");
                }
            }

        });
    }
}