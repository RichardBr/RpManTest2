using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using RpMan.Application.CQRS.Users.Models;
using RpMan.Application.CQRS.Users.Commands.LoginUser;
using RpMan.Application.CQRS.Users.Commands.RegisterUser;
using RpMan.Application.Exceptions;
using RpMan.Domain.Entities;
using RpMan.Domain.ValueObjects;
using RpMan.WebApi.Dtos;

namespace RpMan.WebApi.Controllers
{
    [Route("api/[controller]")]
    [AllowAnonymous]
    public class AuthController : RpManControllerBase
    {
        private readonly IConfiguration _config;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public AuthController(IConfiguration config, IMapper mapper,
            UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _mapper = mapper;
            _userManager = userManager;
            _signInManager = signInManager;
            _config = config;
        }


        //public IActionResult Index()
        //{
        //    return View();
        //}


        [HttpPost("register")]
        public async Task<IActionResult> Register(UserForRegisterDto userForRegisterDto)
        {
            var userToCreate = _mapper.Map<User>(userForRegisterDto);

            userToCreate.AdAccount = AdAccount.For("SSW\\Trevor");

            IdentityResult result = await _userManager.CreateAsync(userToCreate, userForRegisterDto.Password);

            var userToReturn = _mapper.Map<UserForDetailedDto>(userToCreate);

            if (result.Succeeded)
            {
                // await _userManager.AddToRoleAsync(userToCreate, "Member");

                return CreatedAtRoute( routeName: "GetUser"
                                     , routeValues: new { controller = "Users", id = userToCreate.Id }
                                     , value: userToReturn
                                     );
            }

            IEnumerable<IdentityError> identityErrors = result.Errors;

            return BadRequest(identityErrors);
        }
        [HttpPost("register2")]
        public async Task<IActionResult> Register2([FromBody]RegisterUserCommand command)
        {
            var userToReturn = await Mediator.Send(command);

            return CreatedAtRoute(routeName: "GetUser"
                , routeValues: new { controller = "Users", id = userToReturn.Id }
                , value: userToReturn
            );
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(UserForLoginDto userForLoginDto)
        {
            var user = await _userManager.FindByNameAsync(userForLoginDto.Username);
            var result = await _signInManager.CheckPasswordSignInAsync(user, userForLoginDto.Password, false);

            if (result.Succeeded)
            {
                var appUser = await _userManager.Users.FirstOrDefaultAsync(u => u.NormalizedUserName == userForLoginDto.Username.ToUpper());

                UserForListDto userToReturn = _mapper.Map<UserForListDto>(appUser);

                return Ok(new
                {
                    token = GenerateJwtToken(appUser).Result,
                    user = userToReturn
                });
            }

            return Unauthorized();
        }

        [HttpPost("login2")]
        public async Task<IActionResult> Login2(LoginUserCommand command)
        {
            var appUser = await Mediator.Send(command);

            UserForListDto userToReturn = _mapper.Map<UserForListDto>(appUser);

            return Ok(new
            {
                token = GenerateJwtToken(appUser).Result,
                user = userToReturn
            });
        }

        private async Task<string> GenerateJwtToken(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.UserName)
            };

            var roles = await _userManager.GetRolesAsync(user);

            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetSection("AppSettings:Token").Value));
            var signingCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = signingCredentials
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}