using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;
using FluentValidation.Validators;

namespace RpMan.Application.CQRS.Users.Commands.LoginUser
{
    public class LoginUserCommandValidator : AbstractValidator<LoginUserCommand>
    {
        public LoginUserCommandValidator()
        {
            RuleFor(x => x.Username).MaximumLength(24).NotEmpty();
            RuleFor(x => x.Password).MaximumLength(24).NotEmpty();
        }
    }
}
