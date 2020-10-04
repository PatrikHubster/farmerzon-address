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
        /// Inserts a state.
        /// </summary>
        /// <param name="state">The state which should be inserted into the system.</param>
        /// <returns>
        /// A bad request if the data aren't valid, an ok message if everything was fine or an internal server error if
        /// something went wrong.
        /// </returns>
        /// <response code="200">Insertion was able to execute.</response>
        /// <response code="400">One or more optional parameters were not valid.</response>
        /// <response code="500">Something unexpected happened.</response>
        [Topic("pubsub", "state")]
        [HttpPost]
        [ProducesResponseType(typeof(DTO.SuccessResponse<DTO.StateOutput>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(DTO.ErrorResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(DTO.ErrorResponse), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> PostStateAsync([FromBody] DTO.StateInput state)
        {
            var insertedState = await StateManager.InsertEntityAsync(state);
            return Ok(new DTO.SuccessResponse<DTO.StateOutput>
            {
                Success = true,
                Content = insertedState
            });
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
        [ProducesResponseType(typeof(DTO.SuccessResponse<IEnumerable<DTO.StateOutput>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(DTO.ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(DTO.ErrorResponse), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetStatesAsync([FromQuery] long? stateId, [FromQuery] string name)
        {
            var states = await StateManager.GetEntitiesAsync(id: stateId, name: name);
            return Ok(new DTO.SuccessResponse<IEnumerable<DTO.StateOutput>>
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
        [ProducesResponseType(typeof(DTO.SuccessResponse<IDictionary<string, DTO.StateOutput>>), 
            StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(DTO.ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(DTO.ErrorResponse), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetStatesByAddressIdAsync([FromQuery] IEnumerable<long> addressIds)
        {
            var states = await StateManager.GetEntitiesByAddressIdAsync(addressIds);
            return Ok(new DTO.SuccessResponse<IDictionary<string, DTO.StateOutput>>
            {
                Success = true,
                Content = states
            });
        }
        
        /// <summary>
        /// Updates a state.
        /// </summary>
        /// <param name="stateId">The id of the country to update.</param>
        /// <param name="state">The state which should be inserted into the system.</param>
        /// <returns>
        /// A bad request if the data aren't valid, an ok message if everything was fine or an internal server error if
        /// something went wrong.
        /// </returns>
        /// <response code="200">Update was able to execute.</response>
        /// <response code="400">One or more optional parameters were not valid.</response>
        /// <response code="500">Something unexpected happened.</response>
        [HttpPut]
        [ProducesResponseType(typeof(DTO.SuccessResponse<DTO.StateOutput>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(DTO.ErrorResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(DTO.ErrorResponse), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> PutStateAsync([FromQuery] long stateId, [FromBody] DTO.StateInput state)
        {
            var insertedState = await StateManager.UpdateEntityAsync(stateId, state);
            return Ok(new DTO.SuccessResponse<DTO.StateOutput>
            {
                Success = true,
                Content = insertedState
            });
        }
        
        /// <summary>
        /// Delete a state.
        /// </summary>
        /// <param name="stateId">The id of the state to delete.</param>
        /// <returns>
        /// A bad request if the data aren't valid, an ok message if everything was fine or an internal server error if
        /// something went wrong.
        /// </returns>
        /// <response code="200">Deletion was able to execute.</response>
        /// <response code="400">One or more optional parameters were not valid.</response>
        /// <response code="500">Something unexpected happened.</response>
        [HttpDelete]
        [ProducesResponseType(typeof(DTO.SuccessResponse<DTO.StateOutput>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(DTO.ErrorResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(DTO.ErrorResponse), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteStateAsync([FromQuery] long stateId)
        {
            var deletedState = await StateManager.RemoveEntityByIdAsync(stateId);
            return Ok(new DTO.SuccessResponse<DTO.StateOutput>
            {
                Success = true,
                Content = deletedState
            });
        }
    }
}