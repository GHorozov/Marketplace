using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Marketplace.App.Areas.Administrator.ViewModels.Users;
using Marketplace.App.Infrastructure;
using Marketplace.Domain;
using Marketplace.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Marketplace.App.Areas.Administrator.Controllers
{
    public class UsersController : AdministratorController
    {
        private readonly IMapper mapper;
        private readonly IUserService userService;
        private readonly UserManager<MarketplaceUser> userManager;

        public UsersController(IMapper mapper, IUserService usersService, UserManager<MarketplaceUser> userManager)
        {
            this.mapper = mapper;
            this.userService = usersService;
            this.userManager = userManager;
        }

        [HttpGet]
        public IActionResult All()
        {
            var users = this.userService.GetAllUsers<UserViewModel>().ToList();

            return View(users);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(string id)
        {
            var user = await this.userService.GetUserById(id);
            if(user == null)
            {
                return this.RedirectToAction(nameof(All));
            }

            var userModel = this.mapper.Map<DeleteUserViewModel>(user);
            
            return View(userModel);
        }

        [HttpGet]
        public async Task<IActionResult> Destroy(string id)
        {
            await this.userService.DeleteById(id);

            return  RedirectToAction(nameof(All));
        }

        [HttpGet]
        public IActionResult Roles(string id)
        {
            var userRoles = this.userService.GetUserRoles(id)
                .GetAwaiter()
                .GetResult()
                .ToList();

            var userRolesModel = new RolesViewModel()
            {
                Id = id,
                Roles = userRoles
            };

            return this.View(userRolesModel);
        }

        [HttpGet]
        public async Task<IActionResult> ChangePassword(string id)
        {
            var user = await this.userService.GetUserById(id);
            if (user == null)
            {
                return NotFound();
            }

            var modelResult = this.mapper.Map<AdminChangePasswordInputModel>(user);

            return View(modelResult);
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> ChangePassword(string id, AdminChangePasswordInputModel inputModel)
        {
            if (!ModelState.IsValid)
            {
                return this.View(inputModel);
            }

            var result = await this.userService.ChangePassword(id, inputModel.Password);
            if (result.Succeeded)
            {
                return RedirectToAction(nameof(All));
            }
            else
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }

                return View(inputModel);
            }
        }
    }
}