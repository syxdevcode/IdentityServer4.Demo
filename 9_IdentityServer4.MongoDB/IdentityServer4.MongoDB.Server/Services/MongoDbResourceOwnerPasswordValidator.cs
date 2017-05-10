using IdentityServer4.Models;
using IdentityServer4.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServer4.MongoDB.Server.Services
{
    public class MongoDbResourceOwnerPasswordValidator : IResourceOwnerPasswordValidator
    {
        private readonly IRepository _repository;

        public MongoDbResourceOwnerPasswordValidator(IRepository repository)
        {
            _repository = repository;
        }

        public Task ValidateAsync(ResourceOwnerPasswordValidationContext context)
        {
            if (_repository.ValidatePassword(context.UserName, context.Password))
            {
                return Task.FromResult(new GrantValidationResult(context.UserName, "password"));
            }

            return Task.FromResult(new GrantValidationResult(TokenRequestErrors.InvalidGrant, "invalid custom credential"));
        }
    }
}
