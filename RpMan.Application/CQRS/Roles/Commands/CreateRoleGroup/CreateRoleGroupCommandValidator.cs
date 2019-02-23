using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;

namespace RpMan.Application.CQRS.Roles.Commands.CreateRoleGroup
{
    public class CreateRoleGroupCommandValidator : AbstractValidator<CreateRoleGroupCommand>
    {
        public CreateRoleGroupCommandValidator()
        {
            RuleFor(v => v.Name).NotEmpty();
        }
    }
}
