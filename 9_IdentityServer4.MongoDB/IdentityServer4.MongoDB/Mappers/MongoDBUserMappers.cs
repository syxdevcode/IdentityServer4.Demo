using AutoMapper;
using IdentityServer4.MongoDB.Entities;
using IdentityServer4.Test;
using System;
using System.Collections.Generic;
using System.Text;

namespace IdentityServer4.MongoDB.Mappers
{
    public static class MongoDBUserMappers
    {
        static MongoDBUserMappers()
        {
            Mapper = new MapperConfiguration(cfg => cfg.AddProfile<MongoDBUserMapperProfile>())
                .CreateMapper();
        }

        internal static IMapper Mapper { get; }

        public static TestUser ToModel(this MongoDBUser resource)
        {
            return resource == null ? null : Mapper.Map<TestUser>(resource);
        }

        public static MongoDBUser ToEntity(this TestUser resource)
        {
            return resource == null ? null : Mapper.Map<MongoDBUser>(resource);
        }
    }
}
