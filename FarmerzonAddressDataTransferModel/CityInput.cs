using System.ComponentModel.DataAnnotations;

namespace FarmerzonAddressDataTransferModel
{
    public class CityInput
    {
        [Required]
        public string ZipCode { get; set; }
        [Required]
        public string Name { get; set; }
    }
}