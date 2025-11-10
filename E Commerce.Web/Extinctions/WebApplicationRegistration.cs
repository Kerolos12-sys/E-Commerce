using E_Commerce.Domain.Contracts;
using E_Commerce.Persistence.Data.DbContexts;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace E_Commerce.Web.Extinctions
{
    public static class WebApplicationRegistration
    {

        public static async Task<WebApplication> MigrateDatabaseAsync(this WebApplication app)
        {
            await using var scope = app.Services.CreateAsyncScope();
            var dbContextService = scope.ServiceProvider.GetRequiredService<StoreDbContext>();
            var PendingMigrations= await dbContextService.Database.GetPendingMigrationsAsync();
            // لو فيه Migrations لسه متطبقتش، نطبّقها
            if (PendingMigrations.Any())
             await   dbContextService.Database.MigrateAsync();

            return app;
        }
        public static async Task<WebApplication> SeedDatabaseAsync(this WebApplication app)
        {
           await using var scope = app.Services.CreateAsyncScope();
            var dataInitializerService = scope.ServiceProvider.GetRequiredService<IDataInitializer>();

            // نشغل عملية التهيئة
            await dataInitializerService.InitializeAsync();
            return app;
        }


    }
}
