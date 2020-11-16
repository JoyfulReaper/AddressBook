using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AddressBookDataAccess.DataAccess;
using AddressBookDataAccess.Models.Contact;
using AddressBookDataAccess.Models.People;
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
            Person personDetails = db.GetPersonById(id.Value);

            GeneratePlaceholderValues(personDetails);
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
            return View(personSubmitVM);
        }

        [ActionName("CreatePerson")]
        public IActionResult Create(Person person)
        {
            db.CreatePerson(person);
            return RedirectToAction("Index");
        }
    }
}