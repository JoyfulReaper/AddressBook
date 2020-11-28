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
        //[MinimumElements(1)]
        [Required]
        [DataType(DataType.EmailAddress)]
        [DisplayName("E-mail Address")]
        public string Email { get; set; }
        [DisplayName("Primary E-mail Address")]
        public bool IsPrimary { get; set; }
    }
}
