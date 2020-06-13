namespace FarmerzonAddressDataTransferModel
{
    public class Address
    {
        // primary key
        public long AddressId { get; set; }
        
        // attributes
        public string DoorNumber { get; set; }
        public string Street { get; set; }
    }
}