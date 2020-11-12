using AddressBookDataAccess.Models.Contact;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AddressBookMVC.Models.ViewModels
{
    public class PersonDetailViewModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName 
        {
            get
            {
                return $"{FirstName} {LastName}";
            }
        }
        public List<Email> EmailAddresses { get; set; } = new List<Email>();
        public List<Address> Addresses { get; set; } = new List<Address>();
        public List<PhoneNum> PhoneNumbers { get; set; } = new List<PhoneNum>();
    }
}
