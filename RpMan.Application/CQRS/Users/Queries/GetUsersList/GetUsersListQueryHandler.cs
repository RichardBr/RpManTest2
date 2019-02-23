using System.Collections.Generic;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RpMan.Persistence;
using System.Threading;
using System.Threading.Tasks;

namespace RpMan.Application.CQRS.Users.Queries.GetUsersList
{
    public class GetUsersListQueryHandler : IRequestHandler<GetUsersListQuery, UsersListViewModel>
    {
        private readonly RpManDbContext _context;
        private readonly IMapper _mapper;

        public GetUsersListQueryHandler(RpManDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<UsersListViewModel> Handle(GetUsersListQuery request, CancellationToken cancellationToken)
        {
            return new UsersListViewModel
            {
                Users = await _context.Users.ProjectTo<UserLookupModel>(_mapper.ConfigurationProvider).ToListAsync(cancellationToken)
            };
        }
    }
}
