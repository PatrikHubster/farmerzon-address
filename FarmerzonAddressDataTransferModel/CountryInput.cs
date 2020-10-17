using System.ComponentModel.DataAnnotations;

namespace FarmerzonAddressDataTransferModel
{
    public class CountryInput
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Code { get; set; }
    }
}