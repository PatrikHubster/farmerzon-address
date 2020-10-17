using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace FarmerzonAddressDataAccessModel
{
    public class Country : BaseModel
    {
        // relationships
        [ForeignKey("CountryId")]
        public IList<Address> Addresses { get; set; }

        // attributes
        public string Code { get; set; }
        public string Name { get; set; }
    }
}