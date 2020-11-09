using AddressBookDataAccess.Models.Contact;
using Microsoft.AspNetCore.Hosting.Server.Features;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AddressBookMVC.Models.People
{
    public class PersonSubmitViewModel
    {
        public int PersonId { get; set; }
        public int EmailId { get; set; }
        public int AddressId { get; set; }
        public int PhoneNumberId { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public List<Email> EmailAddresses { get; set; }
        [Required]
        public List<Address> Addresses { get; set; }
        [Required]
        public List<PhoneNum> PhoneNumbers { get; set; }
    }
}
