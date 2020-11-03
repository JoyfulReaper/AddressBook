using System;
using System.Collections.Generic;

namespace AddressBookDataAccess.Models.Contact
{
    /*
    Add bool checker for address. 'IsMailAddress'
    this will check if whether the address is also the person's mail address
    */

    public class Address
    {
        public int Id { get; set; }
        public int PersonId { get; set; }
        public string StreetAddress { get; set; }
        public string City { get; set; }
        public string Suburb { get; set; }
        public string State { get; set; }
        public string PostCode { get; set; }
        public bool IsMailAddress { get; set; }
    }
}
