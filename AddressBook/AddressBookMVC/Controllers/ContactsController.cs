using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AddressBookDataAccess.DataAccess;
using AddressBookDataAccess.Models.People;
using Microsoft.AspNetCore.Mvc;

namespace AddressBookMVC.Controllers
{
    public class ContactsController : Controller
    {
        private readonly IAddressRepository db;

        public ContactsController(IAddressRepository db)
        {
            this.db = db;
        }

        public IActionResult Index()
        {
            List<Person> people = db.GetPeople();

            return View(people);
        }

        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }

            // to be replaced by a proper PersonDetailVM
            var personDetails = db.GetPersonById(id.Value);

            if (personDetails == null)
            {
                return NotFound();
            }

            return View(personDetails);
        }
    }
}