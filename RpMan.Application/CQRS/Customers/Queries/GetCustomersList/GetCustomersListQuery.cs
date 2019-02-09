using MediatR;

namespace RpMan.Application.Customers.Queries.GetCustomersList
{
    public class GetCustomersListQuery : IRequest<CustomersListViewModel>
    {
    }
}
