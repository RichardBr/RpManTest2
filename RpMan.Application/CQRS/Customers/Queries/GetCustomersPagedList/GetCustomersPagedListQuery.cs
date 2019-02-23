using MediatR;
using RpMan.Application.Infrastructure.PagedList;

namespace RpMan.Application.CQRS.Customers.Queries.GetCustomersPagedList
{
    public class GetCustomersPagedListQuery : PagedListParams, IRequest<CustomersPagedListViewModel>
    {
        // filter
        public string FilterId { get; set; }
        public string FilterNameContains { get; set; }

        // sort
        public string SortBy { get; set; } = "id";
        public bool SortByAscending { get; set; } = true;
    }
}
