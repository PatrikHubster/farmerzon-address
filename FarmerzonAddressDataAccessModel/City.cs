using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace FarmerzonAddressDataAccessModel
{
    public class City : BaseModel
    {
        // relationships
        [ForeignKey("CityId")]
        public IList<Address> Addresses { get; set; }

        // attributes
        public string ZipCode { get; set; }
        public string Name { get; set; }
    }
}