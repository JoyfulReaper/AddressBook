using System;
using System.Collections.Generic;

namespace AddresMVC.Models
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
        public List<Email> EmailAddress { get; set; }
        public List<Address> MailAddress { get; set; }
        public List<PhoneNum> PhoneNumber { get; set; }
        
    }
}

