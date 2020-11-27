using System.ComponentModel;

namespace AddressBookMVC.Models.ViewModels
{
    public class AddressViewModel
    {
        [DisplayName("Street Address")]
        public string StreetAddress { get; set; }
        public string City { get; set; }
        public string Suburb { get; set; }
        public string State { get; set; }
        public string PostCode { get; set; }
        [DisplayName("Mailing Address")]
        public bool IsMailAddress { get; set; }
        [DisplayName("Primary Address")]
        public bool IsPrimary { get; set; }
    }
}