using AutoMapper;
using RpMan.Application.Interfaces.Mapping;
using RpMan.Domain.Entities;

namespace RpMan.Application.CQRS.Roles.Models
{
    public class RoleGroupLookupModel : IHaveCustomMapping
    {
        public string Id { get; set; }
        public string Name { get; set; }

        public void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<UserRoleGroup, RoleGroupLookupModel>()
                //.ForMember(cDTO => cDTO.Id, opt => opt.MapFrom(c => c.Id))
                //.ForMember(cDTO => cDTO.Name, opt => opt.MapFrom(c => c.Name))
                ;
        }
    }
}
