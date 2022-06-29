using KursovNBA.Models.DbClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KursovNBA.Clients
{
    public interface IDynamoDbClient
    {
        public Task<UserRequest> GetID(string Id);
        public Task<string> GetLastId();
        public Task<string> LastMod(string userId);
        public Task<bool> PostDate(UserRequest userRequest);


    }
}
