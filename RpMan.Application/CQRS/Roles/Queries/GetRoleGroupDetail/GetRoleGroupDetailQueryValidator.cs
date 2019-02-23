using FluentValidation;

namespace RpMan.Application.CQRS.Roles.Queries.GetRoleGroupDetail
{
    public class GetRoleGroupDetailQueryValidator : AbstractValidator<GetRoleGroupDetailQuery>
    {
        public GetRoleGroupDetailQueryValidator()
        {
            RuleFor(v => v.Id).NotEmpty();
        }
    }
}
