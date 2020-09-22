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
    [Route("state")]
    [ApiController]
    public class StateController : ControllerBase
    {
        private IStateManager StateManager { get; set; }

        public StateController(IStateManager stateManager)
        {
            StateManager = stateManager;
        }
        
        /// <summary>
        /// Request a list of states.
        /// </summary>
        /// <param name="stateId">Optional parameter for querying for states.</param>
        /// <param name="name">Optional parameter for querying for states.</param>
        /// <returns>
        /// A bad request if the data aren't valid, an ok message if everything was fine or an internal server error if
        /// something went wrong.
        /// </returns>
        /// <response code="200">Query was able to execute.</response>
        /// <response code="400">One or more optional parameters were not valid.</response>
        /// <response code="500">Something unexpected happened.</response>
        [HttpGet]
        [ProducesResponseType(typeof(DTO.SuccessResponse<IList<DTO.StateOutput>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(DTO.ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(DTO.ErrorResponse), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetCitiesAsync([FromQuery] long? stateId, [FromQuery] string name)
        {
            var states = await StateManager.GetEntitiesAsync(stateId, name);
            return Ok(new DTO.SuccessResponse<IList<DTO.StateOutput>>
            {
                Success = true,
                Content = states
            });
        }
        
        /// <summary>
        /// Request a list of states.
        /// </summary>
        /// <param name="addressIds">Find states to the listed address ids.</param>
        /// <returns>
        /// A bad request if the data aren't valid, an ok message if everything was fine or an internal server error if
        /// something went wrong.
        /// </returns>
        /// <response code="200">Query was able to execute.</response>
        /// <response code="400">Article ids were invalid.</response>
        /// <response code="500">Something unexpected happened.</response>
        [HttpGet("get-by-address-id")]
        [ProducesResponseType(typeof(DTO.SuccessResponse<IDictionary<string, DTO.StateOutput>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(DTO.ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(DTO.ErrorResponse), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetCountriesByAddressIdAsync([FromQuery] IEnumerable<long> addressIds)
        {
            var states = await StateManager.GetEntitiesByAddressIdAsync(addressIds);
            return Ok(new DTO.SuccessResponse<IDictionary<string, DTO.StateOutput>>
            {
                Success = true,
                Content = states
            });
        }
    }
}