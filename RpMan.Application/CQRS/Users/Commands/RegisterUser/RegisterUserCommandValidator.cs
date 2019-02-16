using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;
using FluentValidation.Validators;

namespace RpMan.Application.CQRS.Users.Commands.RegisterUser
{
    public class RegisterUserCommandValidator : AbstractValidator<RegisterUserCommand>
    {
        /*
        public UpdateCustomerCommandValidator()
        {
            RuleFor(x => x.Id).MaximumLength(5).NotEmpty();
            RuleFor(x => x.Address).MaximumLength(60);
            RuleFor(x => x.City).MaximumLength(15);
            RuleFor(x => x.CompanyName).MaximumLength(40).NotEmpty();
            RuleFor(x => x.ContactName).MaximumLength(30);
            RuleFor(x => x.ContactTitle).MaximumLength(30);
            RuleFor(x => x.Country).MaximumLength(15);
            RuleFor(x => x.Fax).MaximumLength(24).NotEmpty();
            RuleFor(x => x.Phone).MaximumLength(24).NotEmpty();
            RuleFor(x => x.PostalCode).MaximumLength(10);
            RuleFor(x => x.Region).MaximumLength(15);

            RuleFor(c => c.PostalCode).Matches(@"^\d{4}$")
                .When(c => c.Country == "Australia")
                .WithMessage("Australian Postcodes have 4 digits");

            RuleFor(c => c.Phone)
                .Must(HaveQueenslandLandLine)
                .When(c => c.Country == "Australia" && c.PostalCode.StartsWith("4"))
                .WithMessage("Customers in QLD require at least one QLD landline.");
        }

        private static bool HaveQueenslandLandLine(UpdateCustomerCommand model, string phoneValue, PropertyValidatorContext ctx)
        {
            return model.Phone.StartsWith("07") || model.Fax.StartsWith("07");
        }
        */
        public RegisterUserCommandValidator()
        {
            RuleFor(x => x.Username).MaximumLength(24).NotEmpty();
            RuleFor(x => x.Password).MaximumLength(24).NotEmpty();
            RuleFor(x => x.Gender).MaximumLength(24).NotEmpty();
            RuleFor(x => x.KnownAs).MaximumLength(24).NotEmpty();
            RuleFor(x => x.DateOfBirth).NotEmpty().Must(BeValidDateOfBirth).WithMessage("Please specify a valid DOB date"); ;
            RuleFor(x => x.City).MaximumLength(24).NotEmpty();
            RuleFor(x => x.Country).MaximumLength(24).NotEmpty();
        }
        private bool BeValidDateOfBirth(DateTime dob)
        {
            return dob < DateTime.UtcNow;
        }
    }
}
