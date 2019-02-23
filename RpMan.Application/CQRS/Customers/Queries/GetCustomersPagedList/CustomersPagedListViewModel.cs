using System.Collections.Generic;
using RpMan.Application.Customers.Queries.GetCustomersList;
using RpMan.Application.Infrastructure.PagedList;
using RpMan.Domain.Entities;

namespace RpMan.Application.CQRS.Customers.Queries.GetCustomersPagedList
{
    public class CustomersPagedListViewModel
    {
        public PagedList<CustomerLookupModel> Customers { get; set; }
    }
}
