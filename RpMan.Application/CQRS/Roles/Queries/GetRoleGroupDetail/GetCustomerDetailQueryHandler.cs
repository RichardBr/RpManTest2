using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using RpMan.Application.CQRS.Roles.Models;
using RpMan.Application.Exceptions;
using RpMan.Domain.Entities;
using RpMan.Persistence;

namespace RpMan.Application.CQRS.Roles.Queries.GetRoleGroupDetail
{
    public class GetRoleGroupDetailQueryHandler : IRequestHandler<GetRoleGroupDetailQuery, RoleGroupDetailModel>
    {
        private readonly RpManDbContext _context;
        private readonly IMapper _mapper;

        public GetRoleGroupDetailQueryHandler(RpManDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<RoleGroupDetailModel> Handle(GetRoleGroupDetailQuery request, CancellationToken cancellationToken)
        {
            var entity = await _context.UserRoleGroups.FindAsync(request.Id);

            if (entity == null)
            {
                throw new NotFoundException(nameof(UserRoleGroup), request.Id);
            }

            //return new RoleGroupDetailModel
            //{
            //    Id = entity.Id,
            //    Name = entity.Name
            //};

            return RoleGroupDetailModel.Create(entity);

        }
    }
}
