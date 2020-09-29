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
        /// Inserts a city.
        /// </summary>
        /// <param name="city">The city which should be inserted into the system.</param>
        /// <returns>
        /// A bad request if the data aren't valid, an ok message if everything was fine or an internal server error if
        /// something went wrong in the background.
        /// </returns>
        /// <response code="200">Insertion was able to execute.</response>
        /// <response code="400">One or more optional parameters were not valid.</response>
        /// <response code="500">Something unexpected happened.</response>
        [HttpPost]
        [ProducesResponseType(typeof(DTO.SuccessResponse<DTO.CityOutput>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(DTO.ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(DTO.ErrorResponse), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> PostCityAsync([FromBody] DTO.CityInput city)
        {
            var insertedCity = await CityManager.InsertEntityAsync(city);
            return Ok(new DTO.SuccessResponse<DTO.CityOutput>
            {
                Success = true,
                Content = insertedCity
            });
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
        [ProducesResponseType(typeof(DTO.SuccessResponse<IEnumerable<DTO.CityOutput>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(DTO.ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(DTO.ErrorResponse), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetCitiesAsync([FromQuery] long? cityId, [FromQuery] string zipCode,
            [FromQuery]string name)
        {
            var cities = await CityManager.GetEntitiesAsync(id: cityId, zipCode: zipCode, name: name);
            return Ok(new DTO.SuccessResponse<IEnumerable<DTO.CityOutput>>
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
        [HttpGet("by-address-id")]
        [ProducesResponseType(typeof(DTO.SuccessResponse<IDictionary<string, DTO.CityOutput>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(DTO.ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(DTO.ErrorResponse), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetCitiesByAddressIdAsync([FromQuery] IEnumerable<long> addressIds)
        {
            var cities = await CityManager.GetEntitiesByAddressIdAsync(addressIds);
            return Ok(new DTO.SuccessResponse<IDictionary<string, DTO.CityOutput>>
            {
                Success = true,
                Content = cities
            });
        }
        
        /// <summary>
        /// Update a city.
        /// </summary>
        /// <param name="cityId">The id of the city to update.</param>
        /// <param name="city">The city which should be updated in the system.</param>
        /// <returns>
        /// A bad request if the data aren't valid, an ok message if everything was fine or an internal server error if
        /// something went wrong in the background.
        /// </returns>
        /// <response code="200">Update was able to execute.</response>
        /// <response code="400">One or more optional parameters were not valid.</response>
        /// <response code="500">Something unexpected happened.</response>
        [HttpPut]
        [ProducesResponseType(typeof(DTO.SuccessResponse<DTO.CityOutput>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(DTO.ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(DTO.ErrorResponse), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> PutCityAsync([FromQuery] long cityId, [FromBody] DTO.CityInput city)
        {
            var updatedCity = await CityManager.UpdateEntityAsync(cityId, city);
            return Ok(new DTO.SuccessResponse<DTO.CityOutput>
            {
                Success = true,
                Content = updatedCity
            });
        }

        /// <summary>
        /// Delete a city.
        /// </summary>
        /// <param name="cityId">The id of the city to delete.</param>
        /// <returns>
        /// A bad request if the data aren't valid, an ok message if everything was fine or an internal server error if
        /// something went wrong in the background.
        /// </returns>
        /// <response code="200">Deletion was able to execute.</response>
        /// <response code="400">One or more optional parameters were not valid.</response>
        /// <response code="500">Something unexpected happened.</response>
        [HttpDelete]
        [ProducesResponseType(typeof(DTO.SuccessResponse<DTO.CityOutput>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(DTO.ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(DTO.ErrorResponse), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteCityAsync([FromQuery] long cityId)
        {
            var deletedCity = await CityManager.RemoveEntityByIdAsync(cityId);
            return Ok(new DTO.SuccessResponse<DTO.CityOutput>
            {
                Success = true,
                Content = deletedCity
            });
        }
    }
}