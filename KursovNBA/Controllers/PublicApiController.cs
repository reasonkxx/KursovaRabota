using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KursovNBA.Clients;
using KursovNBA.Models.NBASTATS;



namespace KursovNBA.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class NBAControllers : ControllerBase
    {
        [HttpGet("Stats")]
        public PlayerInfo GetStaticPlayer(string player)
        {
            AverageClient client1 = new AverageClient();
            var res = client1.GetStaticPlayer(player);

            if (res.Result == null || res.Result.Data.Count == 0)
            {
                return null;
            }

            return res.Result.Data.First();

        }

        [HttpGet("Average")]
        public PlayerStatics GetAverage(int playerId, int season)
        {
            AverageClient client1 = new AverageClient();
            var res = client1.GetAverage(playerId, season);
            if (res.Result == null || res.Result.Data.Count == 0)
            {
                return null;
            }
            return res.Result.Data.First();
        }
        [HttpGet("Team")]
        public Team GetStaticTeam(int ID)
        {
            AverageClient client1 = new AverageClient();
            var res = client1.GetStaticTeam(ID);
            if (res.Result == null || ((int)res.Status) == 404)
            {
                return null;
            }
            return res.Result;
        }
        [HttpGet("IDPLAYER")]
        public ID GetID(int Id)
        {
            AverageClient client1 = new AverageClient();
            var res = client1.GetID(Id);
            if (res.Result == null)
            {
                return null;
            }
            return res.Result;
        }
    }
}
