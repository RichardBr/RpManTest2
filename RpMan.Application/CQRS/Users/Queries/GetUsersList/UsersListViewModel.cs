using System;
using System.Collections.Generic;
using System.Text;

namespace RpMan.Application.CQRS.Users.Queries.GetUsersList
{
    public class UsersListViewModel
    {
        public IList<UserLookupModel> Users { get; set; }
    }
}
