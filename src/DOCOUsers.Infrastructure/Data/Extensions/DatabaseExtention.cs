using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace DOCOUsers.Infrastructure.Data.Extensions
{
    public static class DatabaseExtention
    {
        public static void InitialiseDatabase(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            context.Database.MigrateAsync().GetAwaiter().GetResult();
        }
    }
}
