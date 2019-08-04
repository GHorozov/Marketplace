using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Marketplace.App.Areas.Administrator.ViewModels.Users
{
    public class RolesViewModel
    {
        public string Id { get; set; }

        public List<string> Roles { get; set; }
    }
}
