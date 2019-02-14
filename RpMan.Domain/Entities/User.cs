﻿
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using RpMan.Domain.ValueObjects;

namespace RpMan.Domain.Entities
{
    public class User : IdentityUser<int>
    {
        public ICollection<UserRole> UserRoles { get; set; }

        public AdAccount AdAccount { get; set; }
    }
}
