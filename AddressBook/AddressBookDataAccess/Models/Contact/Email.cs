using System;
using System.Collections.Generic;

namespace AddressBookMVC.Models
{
    public class Email
    {
        public int Id { get; set; }  
        public int PersonId { get; set; }      
        public string EmailAddress { get; set; }
    }
}