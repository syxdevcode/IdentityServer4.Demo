using IdentityServer4.Models;
using IdentityServer4.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServer4.MongoDB.Server.Services
{
    public class MongoDbClientStore : IClientStore
    {
        private readonly IRepository _repository;

        public MongoDbClientStore(IRepository repository)
        {
            _repository = repository;
        }

        public Task<Client> FindClientByIdAsync(string clientId)
        {
            var client = _repository.GetClient(clientId);
            if (client == null)
            {
                return Task.FromResult<Client>(null);
            }

            return Task.FromResult(new Client()
            {
                ClientId = client.ClientId,
                AllowedGrantTypes = client.GrantTypes,
                AllowedScopes = client.AllowedScopes,
                RedirectUris = client.RedirectUris,
                ClientSecrets = client.ClientSecrets.Select(s => new Secret(s.Sha256())).ToList()
            });
        }
    }
}
