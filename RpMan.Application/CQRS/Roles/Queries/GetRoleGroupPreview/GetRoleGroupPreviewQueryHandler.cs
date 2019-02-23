using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;

using RpMan.Application.CQRS.Roles.Models;
using RpMan.Persistence;

namespace RpMan.Application.CQRS.Roles.Queries.GetRoleGroupPreview
{
    public class GetRoleGroupPreviewQueryHandler : IRequestHandler<GetRoleGroupPreviewQuery, List<RoleGroupPreviewDto>>
    {
        private readonly RpManDbContext _context;

        public GetRoleGroupPreviewQueryHandler(RpManDbContext context)
        {
            _context = context;
        }

        public Task<List<RoleGroupPreviewDto>> Handle(GetRoleGroupPreviewQuery request, CancellationToken cancellationToken)
        {
            return _context.UserRoleGroups
                           .Select(RoleGroupPreviewDto.Projection)
                           .ToListAsync(cancellationToken)
                ;
        }
    }
}
