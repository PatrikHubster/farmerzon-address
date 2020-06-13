using System.Collections.Generic;

namespace FarmerzonAddressDataAccessModel
{
    public class Country
    {
        // primary key
        public long CountryId { get; set; }
        
        // relationships
        public IList<Address> Addresses { get; set; }

        // attributes
        public string Name { get; set; }
        public string Code { get; set; }
    }
}