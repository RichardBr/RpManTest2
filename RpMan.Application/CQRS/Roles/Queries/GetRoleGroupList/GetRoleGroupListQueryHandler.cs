using System.Linq;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RpMan.Persistence;
using System.Threading;
using System.Threading.Tasks;
using RpMan.Application.CQRS.Roles.Models;
using RpMan.Application.Infrastructure.PagedList;
using RpMan.Domain.Entities;

namespace RpMan.Application.CQRS.Roles.Queries.GetRoleGroupList
{
    public class GetRoleGroupListQueryHandler : IRequestHandler<GetRoleGroupListQuery, RoleGroupListViewModel>
    {
        private readonly RpManDbContext _context;
        private readonly IMapper _mapper;


        public GetRoleGroupListQueryHandler(RpManDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<RoleGroupListViewModel> Handle(GetRoleGroupListQuery request, CancellationToken cancellationToken)
        {
            var query = _context.UserRoleGroups.AsQueryable();

            if (request.FilterId != null)
            {
                query = query. Where(x => x.Id == request.FilterId);
            }

            if (request.FilterNameContains != null)
            {
                query = query.Where(x => x.Name.Contains(request.FilterNameContains));
            }

            switch (request.SortBy.ToLower())
            {
                case "name":
                    query = request.SortByAscending ? query.OrderBy(x => x.Name) : query.OrderByDescending(x => x.Name);
                    break;
                default:
                    query = request.SortByAscending ? query.OrderBy(x => x.Id) : query.OrderByDescending(x => x.Id);
                    break;
            }

            var queryFinal = query.ProjectTo<RoleGroupLookupModel>(_mapper.ConfigurationProvider);

            return new RoleGroupListViewModel
            {
                RoleGroups = await PagedList<RoleGroupLookupModel>.CreateAsync(queryFinal, request.PageNumber, request.PageSize)
            };
        }
    }
}
