using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using RpMan.Domain.Entities;

namespace RpMan.Application.CQRS.Roles.Models
{
    public class RolePreviewDto
    {
        public int Id { get; set; }

        public string Name { get; set; }


        public static Expression<Func<Role, RolePreviewDto>> Projection
        {
            get
            {
                return p => new RolePreviewDto
                {
                    Id = p.Id,
                    Name = p.Name,
                };
            }
        }
    }
}
