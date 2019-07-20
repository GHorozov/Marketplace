using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Marketplace.App.Areas.Administrator.ViewModels.Users;
using Marketplace.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Marketplace.App.Areas.Administrator.Controllers
{
    public class UsersController : AdministratorController
    {
        private readonly IMapper mapper;
        private readonly IUserService userService;

        public UsersController(IMapper mapper, IUserService usersService)
        {
            this.mapper = mapper;
            this.userService = usersService;
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
            var userModel = await this.userService.GetUserById<DeleteUserViewModel>(id);
            if(userModel == null)
            {
                return NotFound();
            }
           
            return View(userModel);
        }

        [HttpGet]
        public async Task<IActionResult> Destroy(string id)
        {
            await this.userService.DeleteById(id);
            return Redirect(nameof(All));
        }
    }
}