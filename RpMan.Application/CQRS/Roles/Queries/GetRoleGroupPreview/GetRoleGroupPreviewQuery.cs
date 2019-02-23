using System;
using System.Collections.Generic;
using System.Text;
using MediatR;
using RpMan.Application.CQRS.Roles.Models;

namespace RpMan.Application.CQRS.Roles.Queries.GetRoleGroupPreview
{
    public class GetRoleGroupPreviewQuery : IRequest<List<RoleGroupPreviewDto>>
    {
        public int Id { get; set; }
    }
}