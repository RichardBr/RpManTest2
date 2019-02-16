using System;
using System.Collections.Generic;
using System.Text;
using MediatR;
using RpMan.Application.CQRS.Users.Models;
using RpMan.Domain.Entities;

namespace RpMan.Application.CQRS.Users.Commands.LoginUser
{
    public class LoginUserCommand : IRequest<User>
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
