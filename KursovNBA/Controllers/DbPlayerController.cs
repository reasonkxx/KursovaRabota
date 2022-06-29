using KursovNBA.Clients;
using KursovNBA.Models.DbClient;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KursovNBA.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DbPlayerController : ControllerBase
    {
        private readonly ILogger<DbPlayerController> _logger;
        private readonly IDynamoDbPlayerClient _dynamoDbClient;

        public DbPlayerController(ILogger<DbPlayerController> logger, IDynamoDbPlayerClient dynamoDbClient)
        {
            _logger = logger;
            _dynamoDbClient = dynamoDbClient;
        }
        [HttpPost("add")]
        public async Task<IActionResult> PostRequestPlayer([FromBody] PlayerInfoDb userRequest)
        {
            
            var result = await _dynamoDbClient.PostPlayer(userRequest);

            if (result == false)
            {
                return BadRequest("Error , Check Console");
            }

            return Ok("Value has been successfuly added to DB");
        }

        [HttpGet("ById")] //Поиск игрока по ID
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetPlayerById([FromQuery] string id)
        {
            var result = await _dynamoDbClient.GetID(id);

            if (result == null)
            {
                return NotFound("Obj not found");
            }

            return Ok(result);
        }
        [HttpGet("LastId")] //Последний активный пользователь
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetlastId()
        {
            var result = await _dynamoDbClient.GetLastId();

            if (result == null)
            {
                return NotFound("Obj not found");
            }

            return Ok(result);
        }

    }
}
