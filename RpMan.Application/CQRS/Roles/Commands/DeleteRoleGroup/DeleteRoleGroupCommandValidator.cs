using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;
using RpMan.Application.Customers.Commands.DeleteCustomer;

namespace RpMan.Application.CQRS.Roles.Commands.DeleteRoleGroup
{
    class DeleteRoleGroupCommandValidator : AbstractValidator<DeleteRoleGroupCommand>
    {
        public DeleteRoleGroupCommandValidator()
        {
            RuleFor(v => v.Id).NotEmpty();
        }
    }
}
