using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using FarmerzonAddressManager.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using DAO = FarmerzonAddressDataAccessModel;
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
        /// Inserts an address.
        /// </summary>
        /// <param name="address">The address which should be inserted into the system.</param>
        /// <returns>
        /// A bad request if the data aren't valid, an ok message if everything was fine or an internal server error if
        /// something went wrong.
        /// </returns>
        /// <response code="200">Insertion was able to execute.</response>
        /// <response code="400">One or more optional parameters were not valid.</response>
        /// <response code="500">Something unexpected happened.</response>
        [HttpPost]
        [ProducesResponseType(typeof(DTO.SuccessResponse<DTO.AddressOutput>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(DTO.ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(DTO.ErrorResponse), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> PostAddressAsync([FromBody] DTO.AddressInput address)
        {
            var userName = User.FindFirst("userName")?.Value;
            var normalizedUserName = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            
            var insertedAddress = await AddressManager.InsertEntityAsync(address, userName, normalizedUserName);
            return Ok(new DTO.SuccessResponse<DTO.AddressOutput>
            {
                Success = true,
                Content = insertedAddress
            });
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
        [ProducesResponseType(typeof(DTO.SuccessResponse<IEnumerable<DTO.AddressOutput>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(DTO.ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(DTO.ErrorResponse), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAddressesAsync([FromQuery] long? addressId, [FromQuery] string doorNumber,
            [FromQuery]string street)
        {
            var addresses =
                await AddressManager.GetEntitiesAsync(id: addressId, doorNumber: doorNumber, street: street);
            return Ok(new DTO.SuccessResponse<IEnumerable<DTO.AddressOutput>>
            {
                Success = true,
                Content = addresses
            });
        }
        
        /// <summary>
        /// Request a list of addresses.
        /// </summary>
        /// <param name="cityIds">Find addresses to the listed city ids.</param>
        /// <returns>
        /// A bad request if the data aren't valid, an ok message if everything was fine or an internal server error if
        /// something went wrong.
        /// </returns>
        /// <response code="200">Query was able to execute.</response>
        /// <response code="400">Article ids were invalid.</response>
        /// <response code="500">Something unexpected happened.</response>
        [HttpGet("by-city-id")]
        [ProducesResponseType(typeof(DTO.SuccessResponse<IDictionary<string, IList<DTO.AddressOutput>>>), 
            StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(DTO.ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(DTO.ErrorResponse), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAddressesByCityIdAsync([FromQuery] IEnumerable<long> cityIds)
        {
            var addresses = await AddressManager.GetEntitiesByCityIdAsync(cityIds);
            return Ok(new DTO.SuccessResponse<IDictionary<string, IList<DTO.AddressOutput>>>
            {
                Success = true,
                Content = addresses
            });
        }
        
        /// <summary>
        /// Request a list of addresses.
        /// </summary>
        /// <param name="countryIds">Find addresses to the listed country ids.</param>
        /// <returns>
        /// A bad request if the data aren't valid, an ok message if everything was fine or an internal server error if
        /// something went wrong.
        /// </returns>
        /// <response code="200">Query was able to execute.</response>
        /// <response code="400">Article ids were invalid.</response>
        /// <response code="500">Something unexpected happened.</response>
        [HttpGet("by-country-id")]
        [ProducesResponseType(typeof(DTO.SuccessResponse<IDictionary<string, IList<DTO.AddressOutput>>>), 
            StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(DTO.ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(DTO.ErrorResponse), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAddressesByCountryIdAsync([FromQuery] IEnumerable<long> countryIds)
        {
            var addresses = await AddressManager.GetEntitiesByCountryIdAsync(countryIds);
            return Ok(new DTO.SuccessResponse<IDictionary<string, IList<DTO.AddressOutput>>>
            {
                Success = true,
                Content = addresses
            });
        }
        
        /// <summary>
        /// Request a list of addresses.
        /// </summary>
        /// <param name="stateIds">Find addresses to the listed state ids.</param>
        /// <returns>
        /// A bad request if the data aren't valid, an ok message if everything was fine or an internal server error if
        /// something went wrong.
        /// </returns>
        /// <response code="200">Query was able to execute.</response>
        /// <response code="400">Article ids were invalid.</response>
        /// <response code="500">Something unexpected happened.</response>
        [HttpGet("by-state-id")]
        [ProducesResponseType(typeof(DTO.SuccessResponse<IDictionary<string, IList<DTO.AddressOutput>>>), 
            StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(DTO.ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(DTO.ErrorResponse), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAddressesByStateIdAsync([FromQuery] IEnumerable<long> stateIds)
        {
            var addresses = await AddressManager.GetEntitiesByStateIdAsync(stateIds);
            return Ok(new DTO.SuccessResponse<IDictionary<string, IList<DTO.AddressOutput>>>
            {
                Success = true,
                Content = addresses
            });
        }
        
        /// <summary>
        /// Request a list of addresses.
        /// </summary>
        /// <param name="normalizedUserNames">Find addresses to the listed normalized user names.</param>
        /// <returns>
        /// A bad request if the data aren't valid, an ok message if everything was fine or an internal server error if
        /// something went wrong.
        /// </returns>
        /// <response code="200">Query was able to execute.</response>
        /// <response code="400">Article ids were invalid.</response>
        /// <response code="500">Something unexpected happened.</response>
        [HttpGet("by-normalized-user-name")]
        [ProducesResponseType(typeof(DTO.SuccessResponse<IDictionary<string, IList<DTO.AddressOutput>>>), 
            StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(DTO.ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(DTO.ErrorResponse), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAddressesByNormalizedUsernamesAsync(
            [FromQuery] IEnumerable<string> normalizedUserNames)
        {
            var addresses = await AddressManager.GetEntitiesByNormalizedUserNamesAsync(normalizedUserNames);
            return Ok(new DTO.SuccessResponse<IDictionary<string, IList<DTO.AddressOutput>>>
            {
                Success = true,
                Content = addresses
            });
        }
        
        /// <summary>
        /// Update an address.
        /// </summary>
        /// <param name="addressId">The id of the address to update.</param>
        /// <param name="address">The address which should be updated in the system.</param>
        /// <returns>
        /// A bad request if the data aren't valid, an ok message if everything was fine or an internal server error if
        /// something went wrong in the background.
        /// </returns>
        /// <response code="200">Update was able to execute.</response>
        /// <response code="400">One or more optional parameters were not valid.</response>
        /// <response code="500">Something unexpected happened.</response>
        [HttpPut]
        [ProducesResponseType(typeof(DTO.SuccessResponse<DTO.AddressOutput>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(DTO.ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(DTO.ErrorResponse), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateAddress([FromQuery] long addressId, [FromBody] DTO.AddressInput address)
        {
            var userName = User.FindFirst("userName")?.Value;
            var normalizedUserName = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var updatedAddress = 
                await AddressManager.UpdateEntityAsync(addressId, address, userName, normalizedUserName);
            return Ok(new DTO.SuccessResponse<DTO.AddressOutput>
            {
                Success = true,
                Content = updatedAddress
            });
        }

        /// <summary>
        /// Delete an address.
        /// </summary>
        /// <param name="addressId">The id of the address to delete.</param>
        /// <returns>
        /// A bad request if the data aren't valid, an ok message if everything was fine or an internal server error if
        /// something went wrong.
        /// </returns>
        /// <response code="200">Deletion was able to execute.</response>
        /// <response code="400">One or more optional parameters were not valid.</response>
        /// <response code="500">Something unexpected happened.</response>
        [HttpDelete]
        [ProducesResponseType(typeof(DTO.SuccessResponse<DTO.AddressOutput>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(DTO.ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(DTO.ErrorResponse), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteAddress([FromQuery] long addressId)
        {
            var userName = User.FindFirst("userName")?.Value;
            var normalizedUserName = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            
            var deletedAddress = await AddressManager.DeleteEntityByIdAsync(addressId, userName, normalizedUserName);
            return Ok(new DTO.SuccessResponse<DTO.AddressOutput>
            {
                Success = true,
                Content = deletedAddress
            });
        }
    }
}