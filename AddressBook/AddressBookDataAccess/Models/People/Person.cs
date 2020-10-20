using System;
using System.Collections;


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
                return $"{firstName} {lastName}";
            }
        }
        public List<EmailAddress> EmailAddress { get; set; }
        public List<MailAddress> MailAddress { get; set; }
        public List<PhoneNumber> PhoneNumber { get; set; }
        
    }
}

