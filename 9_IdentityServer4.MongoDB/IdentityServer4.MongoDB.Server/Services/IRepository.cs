using IdentityServer4.MongoDB.Server.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServer4.MongoDB.Server.Services
{
    public interface IRepository
    {
        MongoDbUser GetUserByUsername(string username);
        MongoDbUser GetUserById(string id);
        bool ValidatePassword(string username, string plainTextPassword);
        MongoDbClient GetClient(string clientId);
    }
}
