using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace RpMan.Application.CQRS.Users.Queries.GetUsersList
{
    public class GetUsersListQuery : IRequest<UsersListViewModel>
    {
    }
}
