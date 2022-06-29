using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.Model;
using KursovNBA.Extensions;
using KursovNBA.Models.DbClient;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KursovNBA.Clients
{
    public class DynamoDbPlayerClient : IDynamoDbPlayerClient, IDisposable
    {
        public string _tableName;
        private readonly IAmazonDynamoDB _dynamoDb;

        public DynamoDbPlayerClient(IAmazonDynamoDB dynamoDB)
        {
            _dynamoDb = dynamoDB;
            _tableName = "FavoritePlayers";
        }

        public async Task<PlayerInfoDb> GetID(string Id)
        {

            var item = new GetItemRequest
            {
                TableName = _tableName,
                Key = new Dictionary<string, AttributeValue>
                    {
                        { "Id", new AttributeValue(Id) }
                    }
            };
            var response = await _dynamoDb.GetItemAsync(item);


            if (response == null || response.Item.Count == 0)
            {
                return null;
            }

            var result = response.Item.ToClass<PlayerInfoDb>();

            return result;

        }

        public async Task<bool> PostPlayer(PlayerInfoDb playerInfo)
        {
            var request = new PutItemRequest
            {
                TableName = _tableName,
                Item = new Dictionary<string, AttributeValue>
                {
                    { "Id", new AttributeValue(s: playerInfo.Id) },
                    { "First_Name", new AttributeValue(s: playerInfo.First_Name) },
                    { "Last_Name", new AttributeValue(s: playerInfo.Last_Name) },
                    { "IdPlayer", new AttributeValue(s: playerInfo.IdPlayer) },
                    { "Position", new AttributeValue(s: playerInfo.Position) },
                    { "Team_Full_Name", new AttributeValue(s: playerInfo.Team_Full_Name) },
                    { "UserId" , new AttributeValue(s: playerInfo.UserId) }
                }
            };

            try
            {
                var response = await _dynamoDb.PutItemAsync(request);
                return response.HttpStatusCode == System.Net.HttpStatusCode.OK;
            }
            catch (Exception e)
            {
                Console.WriteLine("Here is tour error\n" + e);
                return false;
            }

        }

        public Task<PlayerInfoDb> GetData(string Id)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
        public async Task<string> GetLastId()
        {
            var data = new List<UserRequest> { };
            var request = new ScanRequest
            {
                TableName = _tableName
            };
            var response = await _dynamoDb.ScanAsync(request);

            if (response == null || response.Items.Count == 0)
            {
                return null;
            }

            foreach (var item in response.Items)
            {
                data.Add(item.ToClass<UserRequest>());
            }

            string result = data.Max(x => int.Parse(x.Id)).ToString();

            return result;
        }
    }
}
