using System.ComponentModel.DataAnnotations.Schema;

namespace FarmerzonAddressDataAccessModel
{
    public class Address : BaseModel
    {
        // foreign keys
        [ForeignKey("City")]
        public long CityId { get; set; }
        [ForeignKey("Country")]
        public long CountryId { get; set; }
        [ForeignKey("State")]
        public long StateId { get; set; }
        [ForeignKey("Person")]
        public long PersonId { get; set; }
        
        // relationships
        public City City { get; set; }
        public Country Country { get; set; }
        public State State { get; set; }
        public Person Person { get; set; }

        // attributes
        public string DoorNumber { get; set; }
        public string Street { get; set; }
    }
}