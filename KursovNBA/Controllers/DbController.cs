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
    public class DbController : ControllerBase
    {
        private readonly ILogger<DbController> _logger;
        private readonly IDynamoDbClient _dynamoDbClient;

        public DbController(ILogger<DbController> logger, IDynamoDbClient dynamoDbClient)
        {
            _logger = logger;
            _dynamoDbClient = dynamoDbClient;
        }


        [HttpGet("Item")] //Поиск пользователя по ID
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetRequestHistory([FromQuery] string id)
        {
            var result = await _dynamoDbClient.GetID(id);

            if (result == null)
            {
                return NotFound("Obj not found");
            }

            var ResponseToUser = new UserRequest
            {
                Id = result.Id,
                UserId = result.UserId,
                Request = result.Request,
            };
            return Ok(ResponseToUser);
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

        [HttpGet("LastMode")] //последний запрос пользователя
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetLastUserModee(string userId)
        {
            var result = await _dynamoDbClient.LastMod(userId);

            if (result == null)
            {
                return NotFound("Obj not found");
            }
            return Ok(result);

        }




        [HttpPost("Add")] //Добавление пользователя  в БД
        public async Task<IActionResult> PostRequestToHistory([FromBody] UserRequest userRequest)
        {

            var result = await _dynamoDbClient.PostDate(userRequest);

            if (result == false)
            {
                return BadRequest("Check console log");
            }

            return Ok("Value has been successfuly added to DB");
        }

    }
}

