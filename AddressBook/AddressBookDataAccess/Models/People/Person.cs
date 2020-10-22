using AddressBookDataAccess.Models.Contact;
using System;
using System.Collections.Generic;

namespace AddressBookDataAccess.Models.People
{
    /*
    Add an interface to seperate or include in this class

    Or use Dependancy injections
    */

    public class Person
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
        public List<Email> EmailAddresses { get; set; }
        public List<Address> Addresses { get; set; }
        public List<PhoneNum> PhoneNumbers { get; set; }

    }
}

