using System;
using System.Collections.Generic;
using System.Text;
using MediatR;

namespace RpMan.Application.CQRS.Roles.Commands.CreateRoleGroup
{
    public class CreateRoleGroupCommand : IRequest<int>
    {
        public string Name { get; set; }
    }
}
