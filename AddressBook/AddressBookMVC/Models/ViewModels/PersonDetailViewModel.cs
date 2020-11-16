using AddressBookDataAccess.Models.Contact;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace AddressBookMVC.Models.ViewModels
{
    public class PersonDetailViewModel
    {
        // public int Id { get; set; } // probably not needed
        [DisplayName("First Name:")]
        public string FirstName { get; set; }
        [DisplayName("Last Name:")]
        public string LastName { get; set; }
        public string FullName 
        {
            get
            {
                return $"{FirstName} {LastName}";
            }
        }
        [DisplayName("Email Addresses:")]
        public List<Email> EmailAddresses { get; set; } = new List<Email>();
        public List<Address> Addresses { get; set; } = new List<Address>();
        [DisplayName("Phone Numbers:")]
        public List<PhoneNum> PhoneNumbers { get; set; } = new List<PhoneNum>();
    }
}
