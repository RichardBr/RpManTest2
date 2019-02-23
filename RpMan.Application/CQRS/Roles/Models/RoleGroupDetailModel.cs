using System;
using System.Linq.Expressions;
using RpMan.Domain.Entities;

namespace RpMan.Application.CQRS.Roles.Models
{
    public class RoleGroupDetailModel
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public static Expression<Func<UserRoleGroup, RoleGroupDetailModel>> Projection
        {
            get
            {
                return userRoleGrp => new RoleGroupDetailModel
                {
                    Id = userRoleGrp.Id,
                    Name = userRoleGrp.Name
                };
            }
        }

        public static RoleGroupDetailModel Create(UserRoleGroup userRoleGroup)
        {
            return Projection.Compile().Invoke(userRoleGroup);
        }
    }
}
