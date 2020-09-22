using AutoMapper;

using DAO = FarmerzonAddressDataAccessModel;
using DTO = FarmerzonAddressDataTransferModel;

namespace FarmerzonAddressManager.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Address
            CreateMap<DAO.Address, DTO.AddressOutput>();

            // City
            CreateMap<DAO.City, DTO.CityOutput>();
            CreateMap<DTO.CityInput, DAO.City>();

            // Country
            CreateMap<DAO.Country, DTO.CountryOutput>();

            // Person
            CreateMap<DAO.Person, DTO.PersonOutput>();

            // State
            CreateMap<DAO.State, DTO.StateOutput>();
        }
    }
}
