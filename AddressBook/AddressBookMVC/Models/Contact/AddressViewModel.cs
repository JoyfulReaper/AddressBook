using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace AddressBookMVC.Models.ViewModels
{
    public class AddressViewModel
    {
        [Required]
        [DisplayName("Street Address")]
        public string StreetAddress { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string Suburb { get; set; }
        [Required]
        public string State { get; set; }
        [Required]
        [DisplayName("Post Code")]
        public string PostCode { get; set; }
        [Required]
        [DisplayName("Mailing Address")]
        public bool IsMailAddress { get; set; }
        [Required]
        [DisplayName("Primary Address")]
        public bool IsPrimary { get; set; }
    }
}