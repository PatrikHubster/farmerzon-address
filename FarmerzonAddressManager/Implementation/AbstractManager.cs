using AutoMapper;

namespace FarmerzonAddressManager.Implementation
{
    public abstract class AbstractManager
    {
        protected IMapper Mapper { get; set; }

        public AbstractManager(IMapper mapper)
        {
            Mapper = mapper;
        }
    }
}