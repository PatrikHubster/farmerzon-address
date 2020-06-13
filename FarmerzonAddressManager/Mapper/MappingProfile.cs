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
            CreateMap<DAO.Address, DTO.Address>();
            CreateMap<DTO.Address, DAO.Address>();

            // City
            CreateMap<DAO.City, DTO.City>();
            CreateMap<DTO.City, DAO.City>();

            // Country
            CreateMap<DAO.Country, DTO.Country>();
            CreateMap<DTO.Country, DAO.Country>();
            
            // Person
            CreateMap<DAO.Person, DTO.Person>();
            CreateMap<DTO.Person, DAO.Person>();
            
            // State
            CreateMap<DAO.State, DTO.State>();
            CreateMap<DTO.State, DAO.State>();
        }
    }
}
