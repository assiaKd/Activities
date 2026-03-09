using Activities.Domain;
using Activities.Infrastructure.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
namespace Activities.Infrastructure
{
    public static class InfrastructureServices
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration config)
        {
            //services.AddScoped<IUserRepository, SqlUserRepository>();
            services.AddDbContext<AppDbContext>(options =>
             options.UseSqlite(config.GetConnectionString("DefaultConnection"))
         );
            return services;
        }


        public static async Task InitializeDatabaseAsync(this IServiceProvider services)
        {
            using var scope = services.CreateScope();
            var scopedServices = scope.ServiceProvider;

            var context = scopedServices.GetRequiredService<AppDbContext>();
            var userManager = scopedServices.GetRequiredService<UserManager<User>>();
            var env = services.GetRequiredService<IHostEnvironment>();

            await DbInitializer.SeedAsync(context, userManager, env);
        }

        public static async Task ApplyMigrationsAsync(IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            await context.Database.MigrateAsync();
        }
    }
}
