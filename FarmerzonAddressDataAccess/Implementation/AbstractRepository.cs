namespace FarmerzonAddressDataAccess.Implementation
{
    public abstract class AbstractRepository
    {
        protected FarmerzonAddressContext Context { get; set; }

        protected AbstractRepository(FarmerzonAddressContext context)
        {
            Context = context;
        }
    }
}