using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using RpMan.Domain.Entities;

namespace RpMan.Application.CQRS.Roles.Models
{
    public class RoleGroupPreviewDto
    {
        public RoleGroupPreviewDto()
        {
            Roles = new List<RolePreviewDto>();
        }

        public int Id { get; set; }

        public string Name { get; set; }


        public ICollection<RolePreviewDto> Roles { get; set; }

        public static Expression<Func<UserRoleGroup, RoleGroupPreviewDto>> Projection
        {
            get
            {
                return c => new RoleGroupPreviewDto
                {
                    Id = c.Id,
                    Name = c.Name,
                    Roles = c.UserRoleGroupsRoles
                             .Select(x => x.Role)
                             .AsQueryable()
                             .Select(RolePreviewDto.Projection)
                             //.Take(5)
                             .OrderBy(p => p.Name)
                             .ToList()
                };
            }
        }
    }
}
