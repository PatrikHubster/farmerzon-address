namespace FarmerzonAddressDataAccessModel
{
    public class Person
    {
        // primary key
        public long PersonId { get; set; }

        // relationship
        public Address Address { get; set; }
        
        // attributes
        public string UserName { get; set; }
        public string NormalizedUserName { get; set; }
    }
}