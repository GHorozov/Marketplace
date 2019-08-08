using Marketplace.App.ViewModels.Contact;
using Marketplace.Domain;
using Marketplace.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Marketplace.App.Controllers
{
    [Authorize]
    public class ContactController : Controller
    {
        private readonly IMessageService messageService;
        private readonly UserManager<MarketplaceUser> userManager;

        public ContactController(IMessageService messageService, UserManager<MarketplaceUser> userManager)
        {
            this.messageService = messageService;
            this.userManager = userManager;
        }

        [HttpGet]
        public IActionResult Contact()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> Contact(ContactInputModel inputModel)
        {
            if (!ModelState.IsValid)
            {
                return this.View(inputModel);
            }

            var user = await this.userManager.GetUserAsync(HttpContext.User);
            var result = await this.messageService.Create(user.Id, inputModel.Name, inputModel.Email, inputModel.Phone, inputModel.Message);
            if (!result)
            {
                //to do:
                return this.Redirect("/");
            }

            return this.Redirect("/");
        }
    }
}
