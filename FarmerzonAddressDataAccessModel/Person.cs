using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace FarmerzonAddressDataAccessModel
{
    public class Person : BaseModel
    {
        // relationship
        [ForeignKey("PersonId")]
        public IList<Address> Addresses { get; set; }
        
        // attributes
        public string UserName { get; set; }
        public string NormalizedUserName { get; set; }
    }
}