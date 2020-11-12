using System;
using System.Collections.Generic;

namespace AddressBookDataAccess.Models.Contact
{
    public class Email
    {
        public int Id { get; set; }
        public int PersonId { get; set; }
        public string EmailAddress { get; set; }
        public bool IsPrimary { get; set; }
    }
}