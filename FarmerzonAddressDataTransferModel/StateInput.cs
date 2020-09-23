using System.ComponentModel.DataAnnotations;

namespace FarmerzonAddressDataTransferModel
{
    public class StateInput
    {
        [Required]
        public string Name { get; set; }
    }
}