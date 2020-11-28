using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace AddressBookMVC.Models.ViewModels
{
    public class PhoneNumViewModel
    {
        [Required]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$",
                   ErrorMessage = "Entered phone format is not valid.")]
        public int Number { get; set; }
        [DisplayName("Primary number")]
        public bool IsPrimary { get; set; }
    }
}