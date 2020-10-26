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
        
        [HttpPut]
        [ProducesResponseType(typeof(DTO.SuccessResponse<DTO.AddressOutput>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(DTO.ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(DTO.ErrorResponse), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateAddressAsync([FromQuery] long addressId, [FromBody] DTO.AddressInput address)
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
        
        [HttpDelete]
        [ProducesResponseType(typeof(DTO.SuccessResponse<DTO.AddressOutput>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(DTO.ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(DTO.ErrorResponse), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteAddressAsync([FromQuery] long addressId)
        {
            var userName = User.FindFirst("userName")?.Value;
            var normalizedUserName = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            
            var deletedAddress = await AddressManager.RemoveEntityByIdAsync(addressId, userName, normalizedUserName);
            return Ok(new DTO.SuccessResponse<DTO.AddressOutput>
            {
                Success = true,
                Content = deletedAddress
            });
        }
    }
}