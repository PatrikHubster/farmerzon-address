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