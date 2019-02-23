using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Identity;
using RpMan.Application.Exceptions;
using RpMan.Domain.Entities;
using RpMan.Persistence;

namespace RpMan.Application.CQRS.Users.Queries.GetUserDetail
{
    public class GetUserDetailQueryHandler : IRequestHandler<GetUserDetailQuery, UserDetailModel>
    {
        private readonly RpManDbContext _context;
        private readonly UserManager<User> _userManager;

        public GetUserDetailQueryHandler(RpManDbContext context, UserManager<User> userManager)
        {
            _userManager = userManager;
            _context = context;
        }

        public async Task<UserDetailModel> Handle(GetUserDetailQuery request, CancellationToken cancellationToken)
        {
            // var entity = await _context.Users.FindAsync(request.Id);
            var entity = await _userManager.FindByIdAsync(request.Id.ToString());

            if (entity == null)
            {
                throw new NotFoundException(nameof(User), request.Id);
            }

            return new UserDetailModel
            {
                Id = entity.Id,
                Username = entity.UserName
            };
        }
    }
}
