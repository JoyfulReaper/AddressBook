using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AddressBookDataAccess.DataAccess;
using AddressBookDataAccess.Models.Contact;
using AddressBookDataAccess.Models.People;
using AddressBookMVC.Models.Contact;
using AddressBookMVC.Models.ViewModels;
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
        public IActionResult Test()
        {
            PersonSubmitViewModel personSubmitVM = new PersonSubmitViewModel();
            return View(personSubmitVM);
        }

        public IActionResult Create()
        {
            PersonSubmitViewModel personSubmitVM = new PersonSubmitViewModel();
            personSubmitVM.EmailAddresses.Add(new EmailViewModel());
            return View(personSubmitVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddOrderItem([Bind("EmailAddresses")] PersonSubmitViewModel order)
        {
            order.EmailAddresses.Add(new EmailViewModel());
            return PartialView("OrderItems", order);
        }

        [ActionName("CreatePerson")]
        public IActionResult Create([Bind("FirstName, LastName, EmailAddresses")] PersonSubmitViewModel person)
        {
            Person tempPerson = new Person()
            {
                FirstName = person.FirstName,
                LastName = person.LastName,
                EmailAddresses = person.EmailAddresses
                    .Select(e => new Email { EmailAddress = e.Email}).ToList() // to be refactored
            };

            db.CreatePerson(tempPerson);
            return RedirectToAction("Index");
        }

        [HttpPost]
        // Binding an email to our person and returning our partial view
        public async Task<IActionResult> AddEmailAddress([Bind("EmailAddresses")] PersonSubmitViewModel person)
        {
            person.EmailAddresses.Add(new EmailViewModel());
            return PartialView("EmailAddresses", person);
        }

        
    }
}