
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KursovNBA.Models;
using Newtonsoft.Json;


using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Upload;
using Google.Apis.Util.Store;
using Google.Apis.YouTube.v3;
using Google.Apis.YouTube.v3.Data;
using System.IO;
using System.Threading;
using System.Reflection;
using KursovNBA.Clients;

namespace KursovNBA.Clients
{
    public class YouTubeApiClient : IYouTubeApiClient
    {
        private readonly YouTubeService _youTubeService;
        //private readonly YouTubeService _youTubeServiceUser;


        public YouTubeApiClient(YouTubeService youTubeService)
        {
            _youTubeService = youTubeService;
            //_youTubeServiceUser
        }

        public async Task<List<Models.Video>> GetSerchByVideoRequest(string request)
        {
            var searchListRequest = _youTubeService.Search.List("id,snippet");
            searchListRequest.Q = request; // Replace with your search term.
            searchListRequest.MaxResults = 5;

            // Call the search.list method to retrieve results matching the specified query term.
            var searchListResponse = await searchListRequest.ExecuteAsync();

            List<Models.Video> result = new List<Models.Video> { };

            foreach (var searchResult in searchListResponse.Items)
            {
                if (searchResult.Id.Kind == "youtube#video")
                {
                    result.Add(new Models.Video
                    {
                        VideoId = searchResult.Id.VideoId,
                        VideoTitle = searchResult.Snippet.Title,
                        ChannelTitle = searchResult.Snippet.ChannelTitle
                    });
                }
            }
            return result;
        }

    }
}
