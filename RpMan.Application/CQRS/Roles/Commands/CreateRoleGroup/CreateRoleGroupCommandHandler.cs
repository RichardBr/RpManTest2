using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using RpMan.Domain.Entities;
using RpMan.Persistence;

namespace RpMan.Application.CQRS.Roles.Commands.CreateRoleGroup
{
    public class CreateRoleGroupCommandHandler : IRequestHandler<CreateRoleGroupCommand, int>
    {
        private readonly RpManDbContext _context;
        public CreateRoleGroupCommandHandler(RpManDbContext context)
        {
            _context = context;
        }

        public async Task<int> Handle(CreateRoleGroupCommand request, CancellationToken cancellationToken)
        {
            var entity = new UserRoleGroup
            {
                Name = request.Name
            };

            _context.UserRoleGroups.Add(entity);

            await _context.SaveChangesAsync(cancellationToken);

            return entity.Id;
        }
    }
}
