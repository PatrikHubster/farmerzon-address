using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace FarmerzonAddressDataAccessModel
{
    public class State : BaseModel
    {
        // relationships
        [ForeignKey("StateId")]
        public IList<Address> Addresses { get; set; }

        // attributes
        public string Name { get; set; }
    }
}