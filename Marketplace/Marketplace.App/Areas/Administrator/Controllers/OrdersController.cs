using Marketplace.App.Areas.Administrator.ViewModels.Orders;
using Marketplace.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Marketplace.App.Areas.Administrator.Controllers
{
    public class OrdersController : AdministratorController
    {
        private readonly IOrderService orderService;

        public OrdersController(IOrderService orderService)
        {
            this.orderService = orderService;
        }

        [HttpGet]
        public IActionResult Orders()
        {
            var resultModel = this.orderService.GetAllOrders<AdminOrderViewModel>().ToList();

            return this.View(resultModel);
        }

        [HttpGet]
        public IActionResult ViewOrder(string id)
        {
            //to do:

            return this.RedirectToAction(nameof(Orders));
        }
    }
}
