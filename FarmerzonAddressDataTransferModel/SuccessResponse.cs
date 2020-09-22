namespace FarmerzonAddressDataTransferModel
{
    public class SuccessResponse<T> : BaseResponse
    {
        public T Content { get; set; }
    }
}