using System.ComponentModel.DataAnnotations;

namespace FarmerzonAddressDataTransferModel
{
    public class AddressInput
    {
        [Required]
        public CityInput City { get; set; }
        [Required]
        public CountryInput Country { get; set; }
        [Required]
        public string DoorNumber { get; set; }
        [Required]
        public string Street { get; set; }
    }
}