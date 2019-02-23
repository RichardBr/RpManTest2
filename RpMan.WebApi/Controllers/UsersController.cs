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
using RpMan.Application.CQRS.Users.Models;
using RpMan.Application.CQRS.Users.Queries.GetUserDetail;
using RpMan.Application.CQRS.Users.Queries.GetUsersList;
using RpMan.Domain.Entities;
using RpMan.Persistence;

namespace RpMan.WebApi.Controllers
{
    [Route("api/[controller]")]
    [AllowAnonymous]
    public class UsersController : RpManControllerBase
    {
        private readonly IConfiguration _config;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly RpManDbContext _context;

        public UsersController(IConfiguration config, IMapper mapper,
            UserManager<User> userManager, SignInManager<User> signInManager,
            RpManDbContext context)
        {
            _mapper = mapper;
            _userManager = userManager;
            _signInManager = signInManager;
            _config = config;
            _context = context;
        }

        // GET api/users
        [HttpGet]
        public async Task<ActionResult<UsersListViewModel>> GetAll()
        {
            return Ok(await Mediator.Send(new GetUsersListQuery()));
        }

        // GET api/customers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserDetailModel>> Get(int id)
        {
            return Ok(await Mediator.Send(new GetUserDetailQuery { Id = id }));
        }

        [HttpGet("getoldway{id}", Name = "GetUser")]
        public async Task<IActionResult> GetOldWay(int id)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
            var userToReturn = _mapper.Map<UserForDetailedDto>(user);

            return Ok(userToReturn);
        }


    }
}