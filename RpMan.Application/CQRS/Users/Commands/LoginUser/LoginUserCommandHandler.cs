using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RpMan.Application.CQRS.Users.Models;
using RpMan.Application.Exceptions;
using RpMan.Domain.Entities;
using RpMan.Persistence;
using RpMan.Application.Customers.Commands.CreateCustomer;
using RpMan.Domain.ValueObjects;

namespace RpMan.Application.CQRS.Users.Commands.LoginUser
{
    public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, User>
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public LoginUserCommandHandler(
            UserManager<User> userManager,
            SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<User> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByNameAsync(request.Username);

            if (user != null)
            {
                var result = await _signInManager.CheckPasswordSignInAsync(user, request.Password, false);

                if (result.Succeeded)
                {
                    var appUser = await _userManager.Users.FirstOrDefaultAsync(u => u.NormalizedUserName == request.Username.ToUpper());
                    return appUser;
                }
            }

            throw new UserLoginException("Failure during an attempt to login a user");
        }
    }
}
