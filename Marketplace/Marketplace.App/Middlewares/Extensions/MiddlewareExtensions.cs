using Microsoft.AspNetCore.Builder;

namespace Marketplace.App.Middlewares.Extensions
{
    public static class MiddlewareExtensions
    {
        public static IApplicationBuilder UseSeedDatabaseMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<SeedDatabaseMiddleware>();
        }
    }
}
