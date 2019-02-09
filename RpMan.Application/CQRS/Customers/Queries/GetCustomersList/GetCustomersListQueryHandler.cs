using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RpMan.Persistence;
using System.Threading;
using System.Threading.Tasks;

namespace RpMan.Application.Customers.Queries.GetCustomersList
{
    public class GetCustomersListQueryHandler : IRequestHandler<GetCustomersListQuery, CustomersListViewModel>
    {
        private readonly RpManDbContext _context;
        private readonly IMapper _mapper;

        public GetCustomersListQueryHandler(RpManDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<CustomersListViewModel> Handle(GetCustomersListQuery request, CancellationToken cancellationToken)
        {
            return new CustomersListViewModel
            {
                Customers = await _context.Customers.ProjectTo<CustomerLookupModel>(_mapper.ConfigurationProvider).ToListAsync(cancellationToken)
            };
        }
    }
}
