using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServer4.MongoDB.Server.Models
{
    public class MongoDbClient
    {
        public ObjectId Id { get; set; }
        public string ClientId { get; set; }
        public List<string> RedirectUris { get; set; }
        public List<string> ClientSecrets { get; set; }
        public IEnumerable<string> GrantTypes { get; set; }
        public List<string> AllowedScopes { get; set; }
    }
}
