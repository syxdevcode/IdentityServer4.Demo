using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServer4.MongoDB.Server.Models
{
    public class MongoDbUser
    {
        public string Id { get; set; }
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public bool EmailVerified { get; set; }
        public bool IsActive { get; set; }
        public string HashedPassword { get; set; }
    }
}
