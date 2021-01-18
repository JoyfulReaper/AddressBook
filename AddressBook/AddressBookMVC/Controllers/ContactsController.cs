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
using X.PagedList;

namespace AddressBookMVC.Controllers
{
    public class ContactsController : Controller
    {
        private readonly IAddressRepository db;

        public ContactsController(IAddressRepository db)
        {
            this.db = db;
        }

        public IActionResult Index(int? page)
        {
            List<Person> people = db.GetPeople();

            int pageSize = 4;
            int pageNumber = (page ?? 1);
            return View(people.ToPagedList(pageNumber, pageSize));
        }

        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }

            // to be replaced by a proper PersonDetailVM
            Person personDetails = db.GetPersonById(id.Value);

            //GeneratePlaceholderValues(personDetails);
            PersonDetailViewModel personVM = MapToPersonDetailVM(personDetails);

            if (personDetails == null)
            {
                return NotFound();
            }

            return View(personVM);
        }

        private PersonDetailViewModel MapToPersonDetailVM(Person personDetails)
        {
            return new PersonDetailViewModel
            {
                FirstName = personDetails.FirstName,
                LastName = personDetails.LastName,
                Addresses = personDetails.Addresses,
                EmailAddresses = personDetails.EmailAddresses,
                PhoneNumbers = personDetails.PhoneNumbers
            };
        }

        private void GeneratePlaceholderValues(Person personDetails)
        {
            personDetails.EmailAddresses = new List<Email>
            {
                new Email {Id = 1, EmailAddress = "test@mail.com", PersonId = personDetails.Id, IsPrimary = true},
                new Email {Id = 2, EmailAddress = "filleremail@pmail.com", PersonId = personDetails.Id, IsPrimary = false }
            };

            personDetails.Addresses = new List<Address>
            {
                new Address {Id = 1, PersonId = personDetails.Id, IsPrimary = true, City = "Acity", Suburb = "Apoiqw", StreetAddress = "872 conan st", PostCode = "1249", State = "Qpfpeo", IsMailAddress = true},
                new Address {Id = 2, PersonId = personDetails.Id, IsPrimary = false, City = "gewokegcity", Suburb = "Apawegoiqw", StreetAddress = "872 sdgs st", PostCode = "1111", State = "bfgr!", IsMailAddress = false},
            };

            personDetails.PhoneNumbers = new List<PhoneNum>
            {
                new PhoneNum {Id = 1, PersonId = personDetails.Id, IsPrimary = true, Number = 102904912},
                new PhoneNum {Id = 2, PersonId = personDetails.Id, IsPrimary = false, Number = 246347232},
                new PhoneNum {Id = 3, PersonId = personDetails.Id, IsPrimary = false, Number = 235234121}
            };
        }



        public IActionResult Create()
        {
            PersonSubmitViewModel personSubmitVM = new PersonSubmitViewModel();
            personSubmitVM.EmailAddresses.Add(new EmailViewModel());
            personSubmitVM.PhoneNumbers.Add(new PhoneNumViewModel());
            personSubmitVM.Addresses.Add(new AddressViewModel());
            return View(personSubmitVM);
        }



        [ActionName("CreatePerson")]
        public IActionResult Create([Bind("FirstName, LastName, EmailAddresses, PhoneNumbers, Addresses")] PersonSubmitViewModel person)
        {
            if (ModelState.IsValid)
            {
                Person tempPerson = new Person()
                {
                    FirstName = person.FirstName,
                    LastName = person.LastName,
                    EmailAddresses = person.EmailAddresses
                    .Select(e => new Email { EmailAddress = e.Email, IsPrimary = e.IsPrimary }).ToList(), // to be refactored
                    PhoneNumbers = person.PhoneNumbers
                .Select(e => new PhoneNum { Number = e.Number }).ToList(),
                    Addresses = person.Addresses
                .Select(e => new Address { StreetAddress = e.StreetAddress }).ToList()
                };

                db.CreatePerson(tempPerson);
                return RedirectToAction("Index");
            }
            return RedirectToAction("Create");
        }

        [HttpPost]
        // Binding an email to our person and returning our partial view
        public async Task<IActionResult> AddEmailAddress([Bind("EmailAddresses")] PersonSubmitViewModel person)
        {
            person.EmailAddresses.Add(new EmailViewModel());
            return PartialView("EmailAddresses", person);
        }

        [HttpPost]
        // Binding a Phone Num to our person and returning our partial view
        public async Task<IActionResult> AddPhoneNumber([Bind("PhoneNumbers")] PersonSubmitViewModel person)
        {
            person.PhoneNumbers.Add(new PhoneNumViewModel());
            return PartialView("PhoneNumbers", person);
        }

        [HttpPost]
        // Binding an address to our person and returning our partial view
        public async Task<IActionResult> AddAddress([Bind("Addresses")] PersonSubmitViewModel person)
        {
            person.Addresses.Add(new AddressViewModel());
            return PartialView("Addresses", person);
        }
    }
}