using System.Collections.Generic;
using RpMan.Application.CQRS.Roles.Models;
using RpMan.Application.Infrastructure.PagedList;
using RpMan.Domain.Entities;

namespace RpMan.Application.CQRS.Roles.Queries.GetRoleGroupList
{
    public class RoleGroupListViewModel
    {
        public PagedList<RoleGroupLookupModel> RoleGroups { get; set; }
    }
}
