using System.Threading;
using System.Threading.Tasks;
using MediatR;
using RpMan.Application.Exceptions;
using RpMan.Domain.Entities;
using RpMan.Persistence;

namespace RpMan.Application.Customers.Queries.GetCustomerDetail
{
    public class GetCustomerDetailQueryHandler : IRequestHandler<GetCustomerDetailQuery, CustomerDetailModel>
    {
        private readonly RpManDbContext _context;

        public GetCustomerDetailQueryHandler(RpManDbContext context)
        {
            _context = context;
        }

        public async Task<CustomerDetailModel> Handle(GetCustomerDetailQuery request, CancellationToken cancellationToken)
        {
            var entity = await _context.Customers
                .FindAsync(request.Id);

            if (entity == null)
            {
                throw new NotFoundException(nameof(Customer), request.Id);
            }

            return new CustomerDetailModel
            {
                Id = entity.CustomerId,
                Address = entity.Address,
                City = entity.City,
                CompanyName = entity.CompanyName,
                ContactName = entity.ContactName,
                ContactTitle = entity.ContactTitle,
                Country = entity.Country,
                Fax = entity.Fax,
                Phone = entity.Phone,
                PostalCode = entity.PostalCode,
                Region = entity.Region
            };
        }
    }
}
