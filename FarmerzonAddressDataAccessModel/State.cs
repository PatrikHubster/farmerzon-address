using System.Collections.Generic;

namespace FarmerzonAddressDataAccessModel
{
    public class State
    {
        // primary key
        public long StateId { get; set; }
        
        // relationships
        public IList<Address> Addresses { get; set; }

        // attributes
        public string Name { get; set; }
    }
}