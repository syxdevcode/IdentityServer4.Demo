using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace IdentityServer4.MongoDB.Entities
{
    public class MongoDBUser
    {
        public ObjectId Id { get; set; }

        /// <summary>
        ///  Gets or sets the subject identifier.
        /// </summary>
        public string SubjectId { get; set; }

        //
        // 摘要:
        //     Gets or sets the username.
        public string Username { get; set; }

        //
        // 摘要:
        //     Gets or sets the password.
        public string Password { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public bool EmailVerified { get; set; }

        public bool IsActive { get; set; }

        public string Role { get; set; }

        //
        // 摘要:
        //     Gets or sets the provider name.
        public string ProviderName { get; set; }
        //
        // 摘要:
        //     Gets or sets the provider subject identifier.
        public string ProviderSubjectId { get; set; }
        //
        // 摘要:
        //     Gets or sets the claims.
        public ICollection<Claim> Claims { get; set; }

    }
}
