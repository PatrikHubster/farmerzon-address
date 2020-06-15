using System.Collections.Generic;
using System.Threading.Tasks;
using FarmerzonAddressManager.Implementation;
using FarmerzonAddressManager.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using DTO = FarmerzonAddressDataTransferModel;

namespace FarmerzonAddress.Controllers
{
    [Authorize]
    [Route("city")]
    [ApiController]
    public class CityController : ControllerBase
    {
        private ICityManager CityManager { get; set; }

        public CityController(ICityManager cityManager)
        {
            CityManager = cityManager;
        } 
        
        /// <summary>
        /// Request a list of cities.
        /// </summary>
        /// <param name="cityId">Optional parameter for querying for cities.</param>
        /// <param name="zipCode">Optional parameter for querying for cities.</param>
        /// <param name="name">Optional parameter for querying for cities.</param>
        /// <returns>
        /// A bad request if the data aren't valid, an ok message if everything was fine or an internal server error if
        /// something went wrong.
        /// </returns>
        /// <response code="200">Query was able to execute.</response>
        /// <response code="400">One or more optional parameters were not valid.</response>
        /// <response code="500">Something unexpected happened.</response>
        [HttpGet]
        [ProducesResponseType(typeof(DTO.ListResponse<DTO.City>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(DTO.ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(DTO.ErrorResponse), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetCitiesAsync([FromQuery]long? cityId, [FromQuery]string zipCode,
            [FromQuery]string name)
        {
            var cities = await CityManager.GetEntitiesAsync(cityId, zipCode, name);
            return Ok(new DTO.ListResponse<DTO.City>
            {
                Success = true,
                Content = cities
            });
        }
        
        /// <summary>
        /// Request a list of cities.
        /// </summary>
        /// <param name="addressIds">Find cities to the listed address ids.</param>
        /// <returns>
        /// A bad request if the data aren't valid, an ok message if everything was fine or an internal server error if
        /// something went wrong.
        /// </returns>
        /// <response code="200">Query was able to execute.</response>
        /// <response code="400">Article ids were invalid.</response>
        /// <response code="500">Something unexpected happened.</response>
        [HttpGet("get-by-address-id")]
        [ProducesResponseType(typeof(DTO.DictionaryResponse<DTO.City>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(DTO.ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(DTO.ErrorResponse), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetCitiesByAddressIdAsync([FromQuery]IEnumerable<long> addressIds)
        {
            var cities = await CityManager.GetEntitiesByAddressIdAsync(addressIds);
            return Ok(new DTO.DictionaryResponse<DTO.City>
            {
                Success = true,
                Content = cities
            });
        }
    }
}