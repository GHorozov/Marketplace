using Marketplace.Data;
using Marketplace.Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using System.Threading.Tasks;

namespace Marketplace.App.Middlewares
{
    public class SeedDatabaseMiddleware
    {
        private const string ADMIN_FIRST_NAME = "Georgi";
        private const string ADMIN_LAST_NAME = "Horozov";
        private const string ADMIN_EMAIL = "admin@gmail.com";
        private const string ADMIN_PASSWORD = "123123";

        private readonly RequestDelegate _next;

        public SeedDatabaseMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, RoleManager<IdentityRole> roleManager, UserManager<MarketplaceUser> userManager, MarketplaceDbContext dbContext)
        {
            if (!await roleManager.RoleExistsAsync(Infrastructure.GlobalConstants.AdministratorRole))
            {
                await roleManager.CreateAsync(new IdentityRole(Infrastructure.GlobalConstants.AdministratorRole));
            }
            if (!await roleManager.RoleExistsAsync(Infrastructure.GlobalConstants.UserRole))
            {
                await roleManager.CreateAsync(new IdentityRole(Infrastructure.GlobalConstants.UserRole));
            }

            if (!userManager.Users.Any())
            {
                var user = new MarketplaceUser()
                {
                    UserName = ADMIN_EMAIL,
                    Email = ADMIN_EMAIL,
                    FirstName = ADMIN_FIRST_NAME,
                    LastName = ADMIN_LAST_NAME,
                    ShoppingCart = new ShoppingCart()
                };

                await userManager.CreateAsync(user, ADMIN_PASSWORD);
                await userManager.AddToRoleAsync(user, Infrastructure.GlobalConstants.AdministratorRole);
                await dbContext.SaveChangesAsync();
            }

            // Call the next delegate/middleware in the pipeline
            await _next(context);
        }
    }
}
