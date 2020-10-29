using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using AddressBookMVC.ViewModels;
using AddressBookDataAccess.DataAccess;
using AddressBookDataAccess.Models.People;

namespace AddressBookMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IAddressRepository db;

        public HomeController(ILogger<HomeController> logger, IAddressRepository db)
        {
            _logger = logger;
            this.db = db;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            /// Code for testing db operations. To be deleted in the future
            var p = new Person
            {
                FirstName = "Guy",
                LastName = "Dude"
            };

            db.CreatePerson(p);
            var x = 1;

            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
