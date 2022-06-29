using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using KursovNBA.Models.NBASTATS;
using Newtonsoft.Json;



namespace KursovNBA.Clients
{
        public class AverageClient
        {
            private static HttpClient client1;
            public static string adress1;
            public AverageClient()
            {
                client1 = new HttpClient();
                adress1 = Constant.adress;
                client1.BaseAddress = new Uri(adress1);
            }
            public async Task<Root1> GetAverage(int playerId, int season)
            {
                var responce1 = await client1.GetAsync($"/api/v1/season_averages?season={season}&player_ids[]={playerId}");
                responce1.EnsureSuccessStatusCode();
                var content1 = responce1.Content.ReadAsStringAsync().Result;
                var result1 = JsonConvert.DeserializeObject<Root1>(content1);
                return result1;
            }
            public async Task<Root> GetStaticPlayer(string player)
            {
                var responce = await client1.GetAsync($"/api/v1/players?search={player}");
                responce.EnsureSuccessStatusCode();
                var content = responce.Content.ReadAsStringAsync().Result;
                var result = JsonConvert.DeserializeObject<Root>(content);
                return result;
            }
            public async Task<Team> GetStaticTeam(int ID)
            {
            var responce3 = await client1.GetAsync($"/api/v1/teams/{ID}");
            responce3.EnsureSuccessStatusCode();
            var content3 = responce3.Content.ReadAsStringAsync().Result;
            var result3 = JsonConvert.DeserializeObject<Team>(content3);
            return result3;
            }
             public async Task<ID> GetID(int ID)
            {
            var responce4 = await client1.GetAsync($"/api/v1/players/{ID}");
            responce4.EnsureSuccessStatusCode();
            var content4 = responce4.Content.ReadAsStringAsync().Result;
            var result4 = JsonConvert.DeserializeObject<ID>(content4);
            return result4;
            }
        }
}
