using System.Linq;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RpMan.Persistence;
using System.Threading;
using System.Threading.Tasks;
using RpMan.Application.Customers.Queries.GetCustomersList;
using RpMan.Application.Infrastructure.PagedList;
using RpMan.Domain.Entities;

namespace RpMan.Application.CQRS.Customers.Queries.GetCustomersPagedList
{
    public class GetCustomersPagedListQueryHandler : IRequestHandler<GetCustomersPagedListQuery, CustomersPagedListViewModel>
    {
        private readonly RpManDbContext _context;
        private readonly IMapper _mapper;


        public GetCustomersPagedListQueryHandler(RpManDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<CustomersPagedListViewModel> Handle(GetCustomersPagedListQuery request, CancellationToken cancellationToken)
        {
            // var query = _context.Customers.ProjectTo<CustomerLookupModel>(_mapper.ConfigurationProvider);

            var query = _context.Customers.AsQueryable();

            if (request.FilterId != null)
            {
                query = query. Where(x => x.CustomerId == request.FilterId);
            }

            if (request.FilterNameContains != null)
            {
                query = query.Where(x => x.CompanyName.Contains(request.FilterNameContains));
            }

            switch (request.SortBy.ToLower())
            {
                case "name":
                    query = request.SortByAscending ? query.OrderBy(x => x.CompanyName) : query.OrderByDescending(x => x.CompanyName);
                    break;
                default:
                    query = request.SortByAscending ? query.OrderBy(x => x.CustomerId) : query.OrderByDescending(x => x.CustomerId);
                    break;
            }

            var queryFinal = query.ProjectTo<CustomerLookupModel>(_mapper.ConfigurationProvider);

            return new CustomersPagedListViewModel
            {
                Customers = await PagedList<CustomerLookupModel>.CreateAsync(queryFinal, request.PageNumber, request.PageSize)
            };
        }
    }
}
