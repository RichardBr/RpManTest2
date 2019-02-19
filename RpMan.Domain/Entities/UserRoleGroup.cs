using System;
using System.Collections.Generic;

namespace RpMan.Domain.Entities
{
    public class UserRoleGroup
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<UserRoleGroupsRole> UserRoleGroupsRoles { get; set; }
    }
}
