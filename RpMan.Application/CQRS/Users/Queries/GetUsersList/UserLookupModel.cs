using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using RpMan.Application.Interfaces.Mapping;
using RpMan.Domain.Entities;


namespace RpMan.Application.CQRS.Users.Queries.GetUsersList
{
    public class UserLookupModel : IHaveCustomMapping
    {
        public string Id { get; set; }
        public string Name { get; set; }

        public void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<User, UserLookupModel>()
                .ForMember(cDTO => cDTO.Id, opt => opt.MapFrom(c => c.Id))
                .ForMember(cDTO => cDTO.Name, opt => opt.MapFrom(c => c.UserName))
                ;
        }
    }
}
