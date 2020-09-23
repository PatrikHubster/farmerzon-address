namespace FarmerzonAddressDataTransferModel
{
    public class AddressOutput
    {
        // primary key
        public long AddressId { get; set; }
        
        // relationships
        public CityOutput City { get; set; }
        public CountryOutput Country { get; set; }
        
        // attributes
        public string DoorNumber { get; set; }
        public string Street { get; set; }
    }
}