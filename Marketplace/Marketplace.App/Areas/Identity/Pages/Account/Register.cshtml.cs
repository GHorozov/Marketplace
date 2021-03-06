﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Marketplace.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System.Linq;
using Marketplace.Services.Interfaces;
using Marketplace.App.ViewModels.ShoppingCart;
using Marketplace.App.Helpers;
using Marketplace.App.Infrastructure;

namespace Marketplace.App.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<MarketplaceUser> _signInManager;
        private readonly UserManager<MarketplaceUser> _userManager;
        private readonly ILogger<RegisterModel> _logger;
        private readonly IEmailSender _emailSender;
        private readonly IShoppingCartService shoppingCartService;

        public RegisterModel(
            UserManager<MarketplaceUser> userManager,
            SignInManager<MarketplaceUser> signInManager,
            ILogger<RegisterModel> logger,
            IEmailSender emailSender,
            IShoppingCartService shoppingCartService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _emailSender = emailSender;
            this.shoppingCartService = shoppingCartService;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        public class InputModel
        {
            [Required]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }

            [Required]
            [StringLength(50, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 2)]
            [Display(Name = "First name")]
            public string FirstName { get; set; }

            [Required]
            [StringLength(50, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 2)]
            [Display(Name = "Last name")]
            public string LastName { get; set; }

            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Confirm password")]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }
        }

        public async void OnGet(string returnUrl = null)
        {
            if (this.User.Identity.IsAuthenticated)
            {
                Response.Redirect("/Home/Error");
            }

            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

            ReturnUrl = returnUrl;
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");
            if (ModelState.IsValid)
            {
                var isAnyUsers = !_userManager.Users.Any();

                var user = new MarketplaceUser
                {
                    UserName = Input.Email,
                    Email = Input.Email,
                    FirstName = Input.FirstName,
                    LastName = Input.LastName,
                    ShoppingCart = new ShoppingCart()
                };

                user.ShoppingCart.UserId = user.Id;

                var result = await _userManager.CreateAsync(user, Input.Password);
                if (result.Succeeded)
                {
                    _logger.LogInformation("User created a new account with password.");

                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);

                    var callbackUrl = Url.Page(
                        "/Account/ConfirmEmail",
                        pageHandler: null,
                        values: new { userId = user.Id, code = code },
                        protocol: Request.Scheme);

                    await _emailSender.SendEmailAsync(Input.Email, "Confirm your email",
                        $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

                    if (isAnyUsers)
                    {
                        await _userManager.AddToRoleAsync(user, GlobalConstants.AdministratorRole);
                    }
                    else
                    {
                        await _userManager.AddToRoleAsync(user, GlobalConstants.UserRole);
                    }

                    await _signInManager.SignInAsync(user, isPersistent: false);


                    var shoppingCart = this.HttpContext.Session
                                                    .GetObjectFromJson<ShoppingCartViewModel[]>(GlobalConstants.ShoppingCartKey) ??
                                                new List<ShoppingCartViewModel>().ToArray();
                    if (shoppingCart != null)
                    {
                        foreach (var product in shoppingCart)
                        {
                            await this.shoppingCartService.AddProductToShoppingCartAsync(product.Id, this.Input.Email, product.Quantity);
                        }

                        this.HttpContext.Session.Remove(GlobalConstants.ShoppingCartKey);
                    }

                    return LocalRedirect(returnUrl);
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }
    }
}
