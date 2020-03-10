using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ASP_Core_MVC_Template.Models;
using Microsoft.Extensions.Configuration;

namespace ASP_Core_MVC_Template.Controllers
{
    public class HomeController : Controller
    {
        private readonly IConfiguration _configuration;

        public HomeController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IActionResult Index()
        {
            // Set domain names that change with environments.
            string hostnamePrefix;
            string iisPrefix;
            var environment = _configuration["Environment"];
            if (environment == "Development")
            {
                hostnamePrefix = "dev-";
                iisPrefix = "dev-iis";
            }
            else if (environment == "Test")
            {
                hostnamePrefix = "test-";
                iisPrefix = "test-iis";
            }
            else // Assume Production
            {
                hostnamePrefix = "";
                iisPrefix = "finance";
            }

            ViewBag.hostnamePrefix = hostnamePrefix;
            ViewBag.iisPrefix = iisPrefix;
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
