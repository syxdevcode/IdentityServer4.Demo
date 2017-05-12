using AutoMapper;
using IdentityServer4.MongoDB.Entities;
using IdentityServer4.Test;
using System.Linq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Claims;

namespace IdentityServer4.MongoDB.Mappers
{
    public class MongoDBUserMapperProfile: Profile
    {
        public MongoDBUserMapperProfile()
        {
            // entity to model
            CreateMap<MongoDBUser, TestUser>(MemberList.Destination)
                    .ForMember(x => x.Claims, opt => opt.MapFrom(src => src.Claims.Select(x => x.Type)));

            // model to entity
            CreateMap<TestUser, MongoDBUser>(MemberList.Source)
                .ForMember(x => x.Claims, opts => opts.MapFrom(src => src.Claims.Select(x => new Claim(x.Type, x.Value))));
        }
    }
}
