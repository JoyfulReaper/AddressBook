using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace AddressBookMVC.Models.ViewModels
{
    public class PhoneNumViewModel
    {
        [DataType(DataType.PhoneNumber)]
        public int Number { get; set; }
        [DisplayName("Primary number")]
        public bool IsPrimary { get; set; }
    }
}