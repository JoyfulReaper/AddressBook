using System;
using System.Collections.Generic;

namespace AddressBookDataAccess.Models.Contact
{
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
        public bool IsPrimary { get; set; }
        public string FullAddress 
        {
            get
            {
                return $"{StreetAddress}, {Suburb} {PostCode}, {City}, {State}";
            }
        }
    }
}
