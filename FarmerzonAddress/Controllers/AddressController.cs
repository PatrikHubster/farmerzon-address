using System.Collections.Generic;
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
        [HttpGet("get-by-city-id")]
        [ProducesResponseType(typeof(DTO.DictionaryResponse<IList<DTO.Address>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(DTO.ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(DTO.ErrorResponse), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAddressesByCityIdAsync([FromQuery]IEnumerable<long> cityIds)
        {
            var addresses = await AddressManager.GetAddressesByCityIdAsync(cityIds);
            return Ok(new DTO.DictionaryResponse<IList<DTO.Address>>
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
        [HttpGet("get-by-country-id")]
        [ProducesResponseType(typeof(DTO.DictionaryResponse<IList<DTO.Address>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(DTO.ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(DTO.ErrorResponse), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAddressesByCountryIdAsync([FromQuery]IEnumerable<long> countryIds)
        {
            var addresses = await AddressManager.GetAddressesByCountryIdAsync(countryIds);
            return Ok(new DTO.DictionaryResponse<IList<DTO.Address>>
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
        [HttpGet("get-by-state-id")]
        [ProducesResponseType(typeof(DTO.DictionaryResponse<IList<DTO.Address>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(DTO.ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(DTO.ErrorResponse), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAddressesByStateIdAsync([FromQuery]IEnumerable<long> stateIds)
        {
            var addresses = await AddressManager.GetAddressesByStateIdAsync(stateIds);
            return Ok(new DTO.DictionaryResponse<IList<DTO.Address>>
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
        [HttpGet("get-by-normalized-user-name")]
        [ProducesResponseType(typeof(DTO.DictionaryResponse<DTO.Address>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(DTO.ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(DTO.ErrorResponse), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAddressesByStateIdAsync([FromQuery]IEnumerable<string> normalizedUserNames)
        {
            var addresses = await AddressManager.GetAddressesByNormalizedUserNamesAsync(normalizedUserNames);
            return Ok(new DTO.DictionaryResponse<DTO.Address>
            {
                Success = true,
                Content = addresses
            });
        }
    }
}