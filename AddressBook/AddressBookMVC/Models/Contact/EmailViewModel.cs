using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AddressBookMVC.Models.Contact
{
    public class EmailViewModel
    {
        [MinimumElements(1)]
        [DataType(DataType.EmailAddress)]
        [DisplayName("E-mail Address")]
        public string Email { get; set; }
        public int PersonId { get; set; }
    }
}
