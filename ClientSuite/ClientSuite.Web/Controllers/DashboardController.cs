using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using ClientSuite.Service;
using ClientSuite.Web.Models;
using System;
using System.Linq.Dynamic.Core;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Http;
using System.Globalization;
using Microsoft.AspNetCore.Authorization;

namespace ClientSuite.Web.Controllers
{
    public class DashboardController : BaseController
    { 

        public DashboardController()
        {
            
        }
        public IActionResult Index()
        {
            DashboardViewModel homeVM = new DashboardViewModel();

            return View(homeVM);
        }



    }
}
