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
        [ProducesResponseType(typeof(DTO.ListResponse<DTO.Country>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(DTO.ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(DTO.ErrorResponse), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetCitiesAsync([FromQuery]long? countryId, [FromQuery]string name,
            [FromQuery]string code)
        {
            var countries = await CountryManager.GetEntitiesAsync(countryId, name, code);
            return Ok(new DTO.ListResponse<DTO.Country>
            {
                Success = true,
                Content = countries
            });
        }
    }
}