using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity;

namespace RpMan.Domain.Entities
{
    public class UserRoleGroupsRole
    {
        public int UserRoleGroupId { get; set; }
        public int RoleId { get; set; }
        public UserRoleGroup UserRoleGroup { get; set; }
        public Role Role { get; set; }
    }
}
