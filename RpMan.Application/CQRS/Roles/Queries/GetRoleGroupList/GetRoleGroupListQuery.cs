using MediatR;
using RpMan.Application.Infrastructure.PagedList;

namespace RpMan.Application.CQRS.Roles.Queries.GetRoleGroupList
{
    public class GetRoleGroupListQuery : PagedListParams, IRequest<RoleGroupListViewModel>
    {
        // filter
        public int? FilterId { get; set; }
        public string FilterNameContains { get; set; }

        // sort
        public string SortBy { get; set; } = "id";
        public bool SortByAscending { get; set; } = true;
    }
}
