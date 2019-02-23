using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RpMan.Application.Exceptions;
using RpMan.Domain.Entities;
using RpMan.Persistence;

namespace RpMan.Application.CQRS.Roles.Commands.UpdateRoleGroup
{
    public class UpdateRoleGroupCommandHandler : IRequestHandler<UpdateRoleGroupCommand, Unit>
    {
            private readonly RpManDbContext _context;

            public UpdateRoleGroupCommandHandler(RpManDbContext context)
            {
                _context = context;
            }

            public async Task<Unit> Handle(UpdateRoleGroupCommand request, CancellationToken cancellationToken)
            {
                var entity = await _context.UserRoleGroups.SingleAsync(c => c.Id == request.Id, cancellationToken);

                if (entity == null)
                {
                    throw new NotFoundException(nameof(UserRoleGroup), request.Id);
                }

                entity.Name = request.Name;

                _context.UserRoleGroups.Update(entity);

                await _context.SaveChangesAsync(cancellationToken);

                return Unit.Value;
            }

    }
}
