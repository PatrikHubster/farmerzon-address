using System.Collections.Generic;

namespace FarmerzonAddressDataTransferModel
{
    public class ListResponse<T> : BaseResponse
    {
        public IList<T> Content { get; set; }
    }
}