using System;
using System.Collections.Generic;

namespace AddresMVC.Models
{
    public class Address
    {
        public int Id { get; set; }
        public string MailAddress { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string PostalCode { get; set; }
    }
}