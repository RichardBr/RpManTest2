using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RpMan.Application.CQRS.User.Login
{
    public class UserForLoginDto
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
