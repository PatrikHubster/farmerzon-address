using System;
using System.Collections.Generic;
using System.Linq.Expressions;
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
    [Route("person")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        private IPersonManager PersonManager { get; set; }

        public PersonController(IPersonManager personManager)
        {
            PersonManager = personManager;
        }
        
        /// <summary>
        /// Request a list of people.
        /// </summary>
        /// <param name="personId">Optional parameter for querying for people.</param>
        /// <param name="userName">Optional parameter for querying for people.</param>
        /// <param name="normalizedUserName">Optional parameter for querying for people.</param>
        /// <returns>
        /// A bad request if the data aren't valid, an ok message if everything was fine or an internal server error if
        /// something went wrong.
        /// </returns>
        /// <response code="200">Query was able to execute.</response>
        /// <response code="400">One or more optional parameters were not valid.</response>
        /// <response code="500">Something unexpected happened.</response>
        [HttpGet]
        [ProducesResponseType(typeof(DTO.SuccessResponse<IEnumerable<DTO.PersonOutput>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(DTO.ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(DTO.ErrorResponse), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetPeopleAsync([FromQuery] long? personId, [FromQuery] string userName,
            [FromQuery] string normalizedUserName)
        {
            var people = await PersonManager.GetEntitiesAsync(id: personId, userName: userName,
                normalizedUserName: normalizedUserName);
            return Ok(new DTO.SuccessResponse<IEnumerable<DTO.PersonOutput>>
            {
                Success = true,
                Content = people
            });
        }
    }
}