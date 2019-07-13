using Marketplace.App.Areas.Administrator.ViewModels.Home;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Marketplace.App.Areas.Administrator.Controllers
{
    public class HomeController : AdministratorController
    {
        public HomeController()
        {

        }

        public IActionResult Index()
        {

            var indexModel = new IndexViewModel()
            {

            };

            return this.View();
        }
    }
}
