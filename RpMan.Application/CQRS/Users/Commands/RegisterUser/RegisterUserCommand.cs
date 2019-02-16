using System;
using System.Collections.Generic;
using System.Text;
using MediatR;
using RpMan.Application.CQRS.Users.Models;

namespace RpMan.Application.CQRS.Users.Commands.RegisterUser
{
    public class RegisterUserCommand : IRequest<UserForDetailedDto>
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Gender { get; set; }
        public string KnownAs { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public DateTime Created { get; set; }
        public DateTime LastActive { get; set; }

        public RegisterUserCommand()
        {
            Created = DateTime.Now;
            LastActive = DateTime.Now;
        }
    }
}
