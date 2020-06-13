using System.Collections.Generic;

namespace FarmerzonAddressDataTransferModel
{
    public class ErrorResponse : BaseResponse
    {
        public IEnumerable<string> Errors { get; set; }
    }
}