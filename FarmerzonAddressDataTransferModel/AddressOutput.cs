namespace FarmerzonAddressDataTransferModel
{
    public class AddressOutput
    {
        // primary key
        public long AddressId { get; set; }
        
        // attributes
        public string DoorNumber { get; set; }
        public string Street { get; set; }
    }
}