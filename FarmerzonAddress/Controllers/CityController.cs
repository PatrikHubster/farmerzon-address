using System.Collections.Generic;
using System.Threading.Tasks;
using Dapr;
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