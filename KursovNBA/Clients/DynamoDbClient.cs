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
    public class DynamoDbClient : IDynamoDbClient, IDisposable
    {
        public string _tableName;
        private readonly IAmazonDynamoDB _dynamoDb;

        public DynamoDbClient(IAmazonDynamoDB dynamoDB)
        {
            _dynamoDb = dynamoDB;
            _tableName = "Request";
        }

        public async Task<UserRequest> GetID(string Id)
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

            var result = response.Item.ToClass<UserRequest>();

            return result;

        }
        public async Task<bool> PostDate(UserRequest data)
        {
            var request = new PutItemRequest
            {
                TableName = _tableName,
                Item = new Dictionary<string, AttributeValue>
                {
                    {"Id", new AttributeValue(s: data.Id)},
                    {"Request", new AttributeValue(s: data.Request)},
                    {"UserId", new AttributeValue(s: data.UserId)}
                }
            };

            try
            {
                var response = await _dynamoDb.PutItemAsync(request);
                return response.HttpStatusCode == System.Net.HttpStatusCode.OK;
            }
            catch (Exception e)
            {
                Console.WriteLine("Here is your error\n" + e);
                return false;
            }

        }
        public void Dispose()
        {
            _dynamoDb.Dispose();
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
        public async Task<string> LastMod(string userId)
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

            string Id = data.Where(u => u.UserId == userId && u.Request[0] == '#').Max(x => int.Parse(x.Id)).ToString();

            string result = data.Where(x => x.Id == Id).Select(x => x.Request).First();

            return result;
        }

    }
}
