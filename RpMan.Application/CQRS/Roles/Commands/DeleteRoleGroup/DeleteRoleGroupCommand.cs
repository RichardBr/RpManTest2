using System;
using System.Collections.Generic;
using System.Text;
using MediatR;

namespace RpMan.Application.CQRS.Roles.Commands.DeleteRoleGroup
{
    public class DeleteRoleGroupCommand : IRequest
    {
        public int Id { get; set; }
    }
}
