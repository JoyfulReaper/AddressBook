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
using AddressBookDataAccess.Models.Contact;

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
                Id = 2,
                FirstName = "qst1",
                //LastName = "feg",
                EmailAddresses = new List<Email>
                {
                    new Email { EmailAddress = "testval3" },
                    new Email { EmailAddress = "testval4" }
                },
                Addresses = new List<Address>
                {
                    new Address{ StreetAddress = "st1", City = "cty1", Suburb = "sb1", State = "s1t", PostCode = "pc1", IsMailAddress = true}
                },
                PhoneNumbers = new List<PhoneNum>
                {
                    new PhoneNum { Number = 12345678 }
                }
            };
            db.CreatePerson(p);

            // Code for testing db operations. To be deleted in the future.
            //db.GetPersonById(1);
            //db.GetPeople();


            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
