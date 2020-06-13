using System.Collections.Generic;

namespace FarmerzonAddressDataAccessModel
{
    public class City
    {
        // primary key
        public long CityId { get; set; }
        
        // relationships
        public IList<Address> Addresses { get; set; }

        // attributes
        public string ZipCode { get; set; }
        public string Name { get; set; }
    }
}