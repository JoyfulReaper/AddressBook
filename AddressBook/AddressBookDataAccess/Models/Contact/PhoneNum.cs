using System;
using System.Collections.Generic;

namespace AddressBookDataAccess.Models.Contact
{
    public class PhoneNum
    {
        public int Id { get; set; }
        public int PersonId { get; set; }
        public int Number { get; set; }
    }
}