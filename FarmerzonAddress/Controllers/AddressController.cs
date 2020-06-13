using System.Threading.Tasks;
using FarmerzonAddressManager.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using DTO = FarmerzonAddressDataTransferModel;

namespace FarmerzonAddress.Controllers
{
    [Authorize]
    [Route("address")]
    [ApiController]
    public class AddressController : ControllerBase
    {
        private IAddressManager AddressManager { get; set; }

        public AddressController(IAddressManager addressManager)
        {
            AddressManager = addressManager;
        }
        
        /// <summary>
        /// Request a list of addresses.
        /// </summary>
        /// <param name="addressId">Optional parameter for querying for addresses.</param>
        /// <param name="doorNumber">Optional parameter for querying for addresses.</param>
        /// <param name="street">Optional parameter for querying for addresses.</param>
        /// <returns>
        /// A bad request if the data aren't valid, an ok message if everything was fine or an internal server error if
        /// something went wrong.
        /// </returns>
        /// <response code="200">Query was able to execute.</response>
        /// <response code="400">One or more optional parameters were not valid.</response>
        /// <response code="500">Something unexpected happened.</response>
        [HttpGet]
        [ProducesResponseType(typeof(DTO.ListResponse<DTO.Address>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(DTO.ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(DTO.ErrorResponse), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAddressesAsync([FromQuery]long? addressId, [FromQuery]string doorNumber,
            [FromQuery]string street)
        {
            var addresses = await AddressManager.GetEntitiesAsync(addressId, doorNumber, street);
            return Ok(new DTO.ListResponse<DTO.Address>
            {
                Success = true,
                Content = addresses
            });
        }
    }
}