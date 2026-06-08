using ECommerce.Domain.Contracts;
using ECommerce.Presistence.Data.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.API.Extensions
{
    public static class WebApplicationRegister
    {
        public static async Task<WebApplication> MigrateDataBaseAsync(this WebApplication app)
        {

            await using var scope = app.Services.CreateAsyncScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<StoreDbContext>();
            var pendingMigrations=await dbContext.Database.GetPendingMigrationsAsync();
            if (pendingMigrations.Any())
            {
               await dbContext.Database.MigrateAsync();
            }
            return app;
        }

        public static async Task<WebApplication> SeedDataAsync(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            var dataIntializer = scope.ServiceProvider.GetRequiredService<IDataIntializer>();
            await dataIntializer.IntializeAsync();
            return app;
        }
    }
}
