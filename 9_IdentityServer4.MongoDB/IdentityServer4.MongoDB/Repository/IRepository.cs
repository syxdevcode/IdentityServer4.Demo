using IdentityServer4.MongoDB.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace IdentityServer4.MongoDB.Repository
{
    public interface IRepository
    {
        Task<MongoDBUser> GetUserByUsername(string username);
        Task<MongoDBUser> GetUserById(string id);
        Task<bool> ValidatePassword(string username, string plainTextPassword);
        Task<Client> GetClient(string clientId);

        Claim[] GetUserClaims(MongoDBUser user);
    }
}
