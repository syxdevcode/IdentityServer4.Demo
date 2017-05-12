﻿// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using IdentityServer4.MongoDB.Configuration;
using IdentityServer4.MongoDB.Entities;
using IdentityServer4.MongoDB.Interfaces;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServer4.MongoDB.DbContexts
{
    public class ConfigurationDbContext : MongoDBContextBase, IConfigurationDbContext
    {
        private IMongoCollection<Client> _clients;
        private IMongoCollection<IdentityResource> _identityResources;
        private IMongoCollection<ApiResource> _apiResources;
        private IMongoCollection<MongoDBUser> _mongoDBUser;

        public ConfigurationDbContext(IOptions<MongoDBConfiguration> settings)
            : base(settings)
        {
            _clients = Database.GetCollection<Client>(Constants.TableNames.Client);
            _identityResources = Database.GetCollection<IdentityResource>(Constants.TableNames.IdentityResource);
            _apiResources = Database.GetCollection<ApiResource>(Constants.TableNames.ApiResource);
            _mongoDBUser = Database.GetCollection<MongoDBUser>(Constants.TableNames.MongoDBUsers);
        }

        public IQueryable<Client> Clients
        {
            get { return _clients.AsQueryable(); }
        }
        public IQueryable<IdentityResource> IdentityResources
        {
            get { return _identityResources.AsQueryable(); }
        }
  
        public IQueryable<ApiResource> ApiResources
        {
            get { return _apiResources.AsQueryable(); }
        }
        
        public IQueryable<MongoDBUser> MongoDBUsers
        {
            get { return _mongoDBUser.AsQueryable(); }
        }

        public async Task AddClient(Client entity)
        {
            await _clients.InsertOneAsync(entity);
        }

        public async Task AddIdentityResource(IdentityResource entity)
        {
            await _identityResources.InsertOneAsync(entity);
        }

        public async Task AddApiResource(ApiResource entity)
        {
            await _apiResources.InsertOneAsync(entity);
        }
        public async Task AddMongoDBUser(MongoDBUser entity)
        {
            await _mongoDBUser.InsertOneAsync(entity);
        }

    }
}