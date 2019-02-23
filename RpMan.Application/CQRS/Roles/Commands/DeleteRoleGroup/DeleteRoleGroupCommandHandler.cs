using System.Linq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using RpMan.Application.Exceptions;
using RpMan.Domain.Entities;
using RpMan.Persistence;

namespace RpMan.Application.CQRS.Roles.Commands.DeleteRoleGroup
{
    class DeleteRoleGroupCommandHandler : IRequestHandler<DeleteRoleGroupCommand>
    {
        private readonly RpManDbContext _context;

        public DeleteRoleGroupCommandHandler(RpManDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(DeleteRoleGroupCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.UserRoleGroups.FindAsync(request.Id);

            if (entity == null)
            {
                throw new NotFoundException(nameof(UserRoleGroup), request.Id);
            }

            var hasOrders = _context.UserRoleGroupsRoles.Any(o => o.UserRoleGroupId == entity.Id);
            if (hasOrders)
            {
                throw new DeleteFailureException(nameof(UserRoleGroup), request.Id, "There are existing roles associated with this RoleGroup.");
            }

            _context.UserRoleGroups.Remove(entity);

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
