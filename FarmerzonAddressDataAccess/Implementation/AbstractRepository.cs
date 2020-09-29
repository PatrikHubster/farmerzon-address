namespace FarmerzonAddressDataAccess.Implementation
{
    public abstract class AbstractRepository
    {
        protected FarmerzonAddressContext Context { get; set; }

        public AbstractRepository(FarmerzonAddressContext context)
        {
            Context = context;
        }
    }
}