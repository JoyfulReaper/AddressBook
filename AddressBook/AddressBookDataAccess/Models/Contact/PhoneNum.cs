using System;
using System.Collections.Generic;

namespace AddressBookMVC.Models
{
    public class PhoneNum
    {
        public int Id { get; set; }
        public int PersonId { get; set; }
        public string Number { get; set; }
    }
}