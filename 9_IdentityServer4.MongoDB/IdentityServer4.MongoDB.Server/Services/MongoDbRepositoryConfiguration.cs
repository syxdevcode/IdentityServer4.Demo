using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServer4.MongoDB.Server.Services
{
    public class MongoDbRepositoryConfiguration
    {
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
}
