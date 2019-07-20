using Microsoft.AspNetCore.Mvc;

namespace Marketplace.App.Areas.Administrator.Controllers
{
    public class HomeController : AdministratorController
    {
        public IActionResult Index()
        {
            return this.View();
        }
    }
}
