using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using RpMan.Application.CQRS.Roles.Commands.CreateRoleGroup;
using RpMan.Application.CQRS.Roles.Commands.UpdateRoleGroup;
using RpMan.Application.CQRS.Roles.Commands.DeleteRoleGroup;
using RpMan.Application.CQRS.Roles.Models;
using RpMan.Application.CQRS.Roles.Queries.GetRoleGroupDetail;
using RpMan.Application.CQRS.Roles.Queries.GetRoleGroupList;
using RpMan.Application.CQRS.Roles.Queries.GetRoleGroupPreview;
using RpMan.Application.Customers.Commands.DeleteCustomer;
using RpMan.Domain.Entities;
using RpMan.Persistence;
using RpMan.WebApi.Helpers;

namespace RpMan.WebApi.Controllers
{
    [Route("api/[controller]")]
    [AllowAnonymous]
    public class RolesController : RpManControllerBase
    {
        private readonly IConfiguration _config;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly RpManDbContext _context;

        public RolesController(IConfiguration config, IMapper mapper,
            UserManager<User> userManager, SignInManager<User> signInManager,
            RpManDbContext context)
        {
            _mapper = mapper;
            _userManager = userManager;
            _signInManager = signInManager;
            _config = config;
            _context = context;
        }


        // GET api/roles/group
        [HttpGet("group")]
        public async Task<ActionResult<RoleGroupListViewModel>> GetGroup([FromQuery]GetRoleGroupListQuery request)
        {
            var response = await Mediator.Send(request);

            Response.AddPagination(response.RoleGroups.CurrentPage
                , response.RoleGroups.PageSize
                , response.RoleGroups.TotalCount
                , response.RoleGroups.TotalPages
            );

            return Ok(response);
        }

        // GET api/roles/group/5
        [HttpGet("group/{id}")]
        public async Task<ActionResult<RoleGroupDetailModel>> GetGroup(int id)
        {
            return Ok(await Mediator.Send(new GetRoleGroupDetailQuery { Id = id }));
        }

        // GET api/roles/group/preview
        [HttpGet("group/preview")]
        public async Task<IActionResult> GetRoleGroupPreview([FromQuery] GetRoleGroupPreviewQuery query)
        {
            var response = await Mediator.Send(query);
            return Ok(response);
        }

        // POST: api/roles/group/create
        [HttpPost("group/create")]
        public async Task<ActionResult<int>> Create([FromBody] CreateRoleGroupCommand command)
        {
            var roleGroupid = await Mediator.Send(command);

            return Ok(roleGroupid);
        }

        // PUT api/role/group/5
        [HttpPut("group/{id}")]
        public async Task<IActionResult> Update(int id, [FromBody]UpdateRoleGroupCommand command)
        {
            await Mediator.Send(command);
            return NoContent();
        }

        // DELETE api/role/group/5
        [HttpDelete("group/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await Mediator.Send(new DeleteRoleGroupCommand { Id = id });
            return NoContent();
        }

    }
}