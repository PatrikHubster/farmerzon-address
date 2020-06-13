using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using DTO = FarmerzonAddressDataTransferModel;

namespace FarmerzonAddressManager.Interface
{
    public interface IAddressManager
    {
        public Task<IList<DTO.Address>> GetEntitiesAsync(long? id, string doorNumber, string street);
    }
}