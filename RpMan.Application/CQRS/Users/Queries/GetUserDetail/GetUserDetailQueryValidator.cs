using FluentValidation;

namespace RpMan.Application.CQRS.Users.Queries.GetUserDetail
{
    public class GetUserDetailQueryValidator : AbstractValidator<GetUserDetailQuery>
    {
        public GetUserDetailQueryValidator()
        {
            RuleFor(v => v.Id).NotEmpty();
        }
    }
}
