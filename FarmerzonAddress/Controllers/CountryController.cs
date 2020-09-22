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
    [Route("country")]
    [ApiController]
    public class CountryController : ControllerBase
    {
        private ICountryManager CountryManager { get; set; }

        public CountryController(ICountryManager countryManager)
        {
            CountryManager = countryManager;
        }
        
        /// <summary>
        /// Request a list of countries.
        /// </summary>
        /// <param name="countryId">Optional parameter for querying for countries.</param>
        /// <param name="name">Optional parameter for querying for countries.</param>
        /// <param name="code">Optional parameter for querying for countries.</param>
        /// <returns>
        /// A bad request if the data aren't valid, an ok message if everything was fine or an internal server error if
        /// something went wrong.
        /// </returns>
        /// <response code="200">Query was able to execute.</response>
        /// <response code="400">One or more optional parameters were not valid.</response>
        /// <response code="500">Something unexpected happened.</response>
        [HttpGet]
        [ProducesResponseType(typeof(DTO.SuccessResponse<IList<DTO.CountryOutput>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(DTO.ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(DTO.ErrorResponse), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetCountriesAsync([FromQuery] long? countryId, [FromQuery] string name,
            [FromQuery] string code)
        {
            var countries = await CountryManager.GetEntitiesAsync(countryId, name, code);
            return Ok(new DTO.SuccessResponse<IList<DTO.CountryOutput>>
            {
                Success = true,
                Content = countries
            });
        }
        
        /// <summary>
        /// Request a list of countries.
        /// </summary>
        /// <param name="addressIds">Find countries to the listed address ids.</param>
        /// <returns>
        /// A bad request if the data aren't valid, an ok message if everything was fine or an internal server error if
        /// something went wrong.
        /// </returns>
        /// <response code="200">Query was able to execute.</response>
        /// <response code="400">Article ids were invalid.</response>
        /// <response code="500">Something unexpected happened.</response>
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
    }
}