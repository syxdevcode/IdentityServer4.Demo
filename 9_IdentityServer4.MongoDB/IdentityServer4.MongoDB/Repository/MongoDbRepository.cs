using IdentityModel;
using IdentityServer4.MongoDB.Configuration;
using IdentityServer4.MongoDB.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace IdentityServer4.MongoDB.Repository
{
    public class MongoDbRepository : IRepository
    {
        private readonly IPasswordHasher<MongoDBUser> _passwordHasher;
        private readonly IMongoDatabase _db;
        private const string UsersCollectionName = "Users";
        private const string ClientsCollectionName = "Clients";


        public MongoDbRepository(IOptions<MongoDBConfiguration> config ,IPasswordHasher<MongoDBUser> passwordHasher)
        {
            _passwordHasher = passwordHasher;
            var client = new MongoClient(config.Value.ConnectionString);
            _db = client.GetDatabase(config.Value.Database);
        }

        public async Task<MongoDBUser> GetUserByUsername(string username)
        {
            var collection = _db.GetCollection<MongoDBUser>(UsersCollectionName);
            var filter = Builders<MongoDBUser>.Filter.Eq(u => u.Username, username);
            return await collection.Find(filter).SingleOrDefaultAsync();
        }

        public async Task<MongoDBUser> GetUserById(string id)
        {
            var collection = _db.GetCollection<MongoDBUser>(UsersCollectionName);
            var filter = Builders<MongoDBUser>.Filter.Eq(u => u.SubjectId, id);
            return await collection.Find(filter).SingleOrDefaultAsync();
        }

        public async Task<bool> ValidatePassword(string username, string plainTextPassword)
        {
            var user = await GetUserByUsername(username);
            if (user == null)
            {
                return false;
            }

            var result = _passwordHasher.VerifyHashedPassword(user, user.Password, plainTextPassword);
            switch (result)
            {
                case PasswordVerificationResult.Success:
                    return true;
                case PasswordVerificationResult.Failed:
                    return false;
                case PasswordVerificationResult.SuccessRehashNeeded:
                    throw new NotImplementedException();
                default:
                    throw new NotImplementedException();
            }
        }

        public async Task<Client> GetClient(string clientId)
        {
            var collection = _db.GetCollection<Client>(ClientsCollectionName);
            var filter = Builders<Client>.Filter.Eq(x => x.ClientId, clientId);
            return await collection.Find(filter).SingleOrDefaultAsync();
        }

        //build claims array from user data
        public Claim[] GetUserClaims(MongoDBUser user)
        {
            return new Claim[]
            {
            new Claim(JwtClaimTypes.Subject, user.SubjectId.ToString() ?? ""),
            new Claim(JwtClaimTypes.Name, (!string.IsNullOrEmpty(user.FirstName) && !string.IsNullOrEmpty(user.LastName)) ? (user.FirstName + " " + user.LastName) : ""),
            new Claim(JwtClaimTypes.GivenName, user.FirstName  ?? ""),
            new Claim(JwtClaimTypes.FamilyName, user.LastName  ?? ""),
            new Claim(JwtClaimTypes.Email, user.Email  ?? ""),
            //new Claim("some_claim_you_want_to_see", user.Some_Data_From_User ?? ""),

            //roles
            new Claim(JwtClaimTypes.Role, user.Role)
            };
        }
    }
}
