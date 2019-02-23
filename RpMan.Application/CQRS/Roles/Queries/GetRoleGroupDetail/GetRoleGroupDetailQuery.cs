using MediatR;
using RpMan.Application.CQRS.Roles.Models;

namespace RpMan.Application.CQRS.Roles.Queries.GetRoleGroupDetail
{
    public class GetRoleGroupDetailQuery : IRequest<RoleGroupDetailModel>
    {
        public int? Id { get; set; }
    }
}
