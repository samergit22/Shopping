using Microsoft.AspNetCore.Identity;
using Shopping.Data;
using Shopping.Models;

namespace HirePlatform.ApplyMigration
{
    public static class UpdateDatabaseExtension
    {
        public async static Task<WebApplication>UpdateDatabase(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            await ApplyMigration.ApplyMigrationsAsync(scope);
            var context = scope.ServiceProvider.GetRequiredService<ShoppingData>();
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();
            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

              // await app.SeedAppData(context);
            //await app.SeedUsersAndRoles(userManager, roleManager);
            return app;
        }
    }
}
