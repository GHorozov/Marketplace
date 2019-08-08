using AutoMapper;
using Marketplace.App.Areas.Administrator.ViewModels.Messages;
using Marketplace.Domain;
using Marketplace.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Marketplace.App.Areas.Administrator.Controllers
{
    public class MessagesController : AdministratorController
    {
        private readonly UserManager<MarketplaceUser> userManager;
        private readonly IMessageService messageService;
        private readonly IMapper mapper;

        public MessagesController(UserManager<MarketplaceUser> userManager, IMessageService messageService, IMapper mapper)
        {
            this.userManager = userManager;
            this.messageService = messageService;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Messages()
        {
            var user = await this.userManager.GetUserAsync(HttpContext.User);
            var resultModel = this.messageService.GetUserMessages<AdminMessageViewModel>(user.Id).ToList();

            return this.View(resultModel);
        }

        [HttpGet]
        public async Task<IActionResult> Read(string id)
        {
            var message = await this.messageService.GetMessageById(id);
            var resultModel = this.mapper.Map<AdminMessageReadViewModel>(message);

            return this.View(resultModel);
        }

        [HttpGet]
        public async Task<IActionResult> MarkAsRead(string id)
        {
            var user = await this.userManager.GetUserAsync(HttpContext.User);
            var result = await this.messageService.MarkAsReadMessageById(user.Id, id); 

            return this.RedirectToAction(nameof(Messages));
        }

        [HttpGet]
        public async Task<IActionResult> Delete(string id)
        {
            var user = await this.userManager.GetUserAsync(HttpContext.User);
            var result = await this.messageService.DeleteMessageById(user.Id, id);
            return this.RedirectToAction(nameof(Messages));
        }

    }
}
