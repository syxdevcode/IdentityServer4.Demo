using IdentityServer4.MongoDB.Server.Models;
using IdentityServer4.MongoDB.Server.Services;
using IdentityServer4.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServer4.MongoDB.Server.UI
{
    public class LoginService
    {
        private readonly IResourceOwnerPasswordValidator _passwordValidator;
        private readonly IRepository _repository;

        public LoginService(IResourceOwnerPasswordValidator passwordValidator, IRepository repository)
        {
            _passwordValidator = passwordValidator;
            _repository = repository;
        }

        public bool ValidateCredentials(string username, string password)
        {
            return _repository.ValidatePassword(username, password);
        }

        public MongoDbUser FindByUsername(string username)
        {
            return _repository.GetUserByUsername(username);
        }
    }
}
