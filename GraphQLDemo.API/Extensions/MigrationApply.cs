using Microsoft.EntityFrameworkCore;

namespace GraphQLDemo.API.Extensions
{
    public static class MigrationExtensions
    {
        /// <summary>
        /// Ensures that all pending migrations are applied to the database.
        /// </summary>
        /// <param name="host">The host to apply the migrations for.</param>
        public static IHost ApplyMigrations<TContext>(this IHost host) where TContext : DbContext
        {
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var dbContext = services.GetRequiredService<TContext>();

                // Apply any pending migrations
                dbContext.Database.Migrate();
            }

            return host;
        }
    }
}