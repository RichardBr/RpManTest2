using System;
using System.Collections.Generic;
using System.Text;
using MediatR;

namespace RpMan.Application.CQRS.Roles.Commands.UpdateRoleGroup
{
    public class UpdateRoleGroupCommand : IRequest
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
