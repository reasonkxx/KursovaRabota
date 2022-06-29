using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KursovNBA.Clients;
using KursovNBA.Models;


namespace KursovNBA.Controllers

{
    [ApiController]
    [Route("[controller]")]
    public class YouTubeApiController : ControllerBase
    {
        private readonly ILogger<YouTubeApiController> _logger;
        private readonly IYouTubeApiClient _youTubeApiClient;

        public YouTubeApiController(ILogger<YouTubeApiController> logger, IYouTubeApiClient youTubeApiClient)
        {
            _logger = logger;
            _youTubeApiClient = youTubeApiClient;
        }

        [HttpGet("videosbyrequest")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetRequest([FromQuery] string request)
        {
            var result = await _youTubeApiClient.GetSerchByVideoRequest(request);

            if (result == null)
            {
                return NotFound("Obj not found");
            }
            return Ok(result);
        }
    }
}

