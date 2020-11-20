using System.Collections.Generic;
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
    [Route("country")]
    [ApiController]
    public class CountryController : ControllerBase
    {
        private ICountryManager CountryManager { get; set; }

        public CountryController(ICountryManager countryManager)
        {
            CountryManager = countryManager;
        }
        
        [HttpPost]
        [ProducesResponseType(typeof(DTO.SuccessResponse<DTO.CountryOutput>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(DTO.ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(DTO.ErrorResponse), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> PostCountryAsync([FromBody] DTO.CountryInput country)
        {
            var insertedCountry = await CountryManager.InsertEntityAsync(country);
            return Ok(new DTO.SuccessResponse<DTO.CountryOutput>
            {
                Success = true,
                Content = insertedCountry
            });
        }
        
        [HttpGet]
        [ProducesResponseType(typeof(DTO.SuccessResponse<IEnumerable<DTO.CountryOutput>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(DTO.ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(DTO.ErrorResponse), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetCountriesAsync([FromQuery] long? countryId, [FromQuery] string code, 
            [FromQuery] string name)
        {
            var countries = await CountryManager.GetEntitiesAsync(id: countryId, code: code, name: name);
            return Ok(new DTO.SuccessResponse<IEnumerable<DTO.CountryOutput>>
            {
                Success = true,
                Content = countries
            });
        }
        
        [HttpGet("get-by-address-id")]
        [ProducesResponseType(typeof(DTO.SuccessResponse<IDictionary<string, DTO.CountryOutput>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(DTO.ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(DTO.ErrorResponse), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetCountriesByAddressIdAsync([FromQuery] IEnumerable<long> addressIds)
        {
            var countries = await CountryManager.GetEntitiesByAddressIdAsync(addressIds);
            return Ok(new DTO.SuccessResponse<IDictionary<string, DTO.CountryOutput>>
            {
                Success = true,
                Content = countries
            });
        }
        
        [HttpPut]
        [ProducesResponseType(typeof(DTO.SuccessResponse<DTO.CountryOutput>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(DTO.ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(DTO.ErrorResponse), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> PutCountryAsync([FromQuery] long countryId, 
            [FromBody] DTO.CountryInput country)
        {
            var updatedCountry = await CountryManager.UpdateEntityAsync(countryId, country);
            return Ok(new DTO.SuccessResponse<DTO.CountryOutput>
            {
                Success = true,
                Content = updatedCountry
            });
        }
        
        [HttpDelete]
        [ProducesResponseType(typeof(DTO.SuccessResponse<DTO.CountryOutput>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(DTO.ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(DTO.ErrorResponse), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteCountryAsync([FromQuery] long countryId)
        {
            var deletedCountry = await CountryManager.RemoveEntityByIdAsync(countryId);
            return Ok(new DTO.SuccessResponse<DTO.CountryOutput>
            {
                Success = true,
                Content = deletedCountry
            });
        }
    }
}