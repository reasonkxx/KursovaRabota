using KursovNBA.Models.DbClient;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KursovNBA.Clients
{
    public interface IDynamoDbPlayerClient
    {
        
        public Task<bool> PostPlayer(PlayerInfoDb playerInfo);
        public Task<string> GetLastId();
        public Task<PlayerInfoDb> GetID(string Id);

    }
}
