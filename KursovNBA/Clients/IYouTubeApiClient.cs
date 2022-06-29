using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Google.Apis.YouTube.v3.Data;
using KursovNBA.Models;


namespace KursovNBA.Clients
{
    public interface IYouTubeApiClient
    {
        public Task<List<Models.Video>> GetSerchByVideoRequest(string request);
    }
}
