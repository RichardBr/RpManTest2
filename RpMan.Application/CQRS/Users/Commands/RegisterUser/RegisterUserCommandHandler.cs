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

namespace RpMan.Application.CQRS.Users.Commands.RegisterUser
{
    public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, UserForDetailedDto>
    {
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;


        public RegisterUserCommandHandler(
            IMapper mapper,
            UserManager<User> userManager)
        {
            _mapper = mapper;
            _userManager = userManager;
        }

        public async Task<UserForDetailedDto> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            var userToCreate = _mapper.Map<User>(request);

            userToCreate.AdAccount = AdAccount.For("SSW\\James");

            IdentityResult result = await _userManager.CreateAsync(userToCreate, request.Password);

            var userToReturn = _mapper.Map<UserForDetailedDto>(userToCreate);

            if (result.Succeeded)
            {
                // await _userManager.AddToRoleAsync(userToCreate, "Member");
                return userToReturn;
            }

            throw new IdentityErrorException("Failure during an attempt to register a user", result.Errors);
        }
    }
}
