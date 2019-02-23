using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;
using FluentValidation.Validators;

namespace RpMan.Application.CQRS.Roles.Commands.UpdateRoleGroup
{
    public class UpdateRoleGroupCommandValidator : AbstractValidator<UpdateRoleGroupCommand>
    {
        public UpdateRoleGroupCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
            RuleFor(x => x.Name).NotEmpty();
        }
    }
}
