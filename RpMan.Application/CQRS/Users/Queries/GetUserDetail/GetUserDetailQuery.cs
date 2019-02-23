using MediatR;

namespace RpMan.Application.CQRS.Users.Queries.GetUserDetail
{
    public class GetUserDetailQuery : IRequest<UserDetailModel>
    {
        public int Id { get; set; }
    }
}
