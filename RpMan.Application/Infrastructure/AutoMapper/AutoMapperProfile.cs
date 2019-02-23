using AutoMapper;
using System.Reflection;
using RpMan.Application.CQRS.Roles.Models;
using RpMan.Application.CQRS.Roles.Queries.GetRoleGroupList;
using RpMan.Application.CQRS.Users.Commands.RegisterUser;
using RpMan.Application.CQRS.Users.Models;
using RpMan.Domain.Entities;

namespace RpMan.Application.Infrastructure.AutoMapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            LoadStandardMappings();
            LoadCustomMappings();
            LoadConverters();
        }

        private void LoadConverters()
        {
            CreateMap<UserForRegisterDto, User>();
            CreateMap<RegisterUserCommand, User>();

            // Lookups
            CreateMap<UserRoleGroup, RoleGroupLookupModel>();
        }

        private void LoadStandardMappings()
        {
            var mapsFrom = MapperProfileHelper.LoadStandardMappings(Assembly.GetExecutingAssembly());

            foreach (var map in mapsFrom)
            {
                CreateMap(map.Source, map.Destination).ReverseMap();
            }
        }

        private void LoadCustomMappings()
        {
            var mapsFrom = MapperProfileHelper.LoadCustomMappings(Assembly.GetExecutingAssembly());

            foreach (var map in mapsFrom)
            {
                map.CreateMappings(this);
            }
        }
    }
}
