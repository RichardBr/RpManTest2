﻿using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using RpMan.Application.Interfaces.Mapping;
using RpMan.Domain.Entities;

namespace RpMan.Application.CQRS.Users.Models
{
    public class UserForDetailedDto : IHaveCustomMapping
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Gender { get; set; }
        public int Age { get; set; }
        public string KnownAs { get; set; }
        public DateTime Created { get; set; }
        public DateTime LastActive { get; set; }
        public string Introduction { get; set; }
        public string LookingFor { get; set; }
        public string Interests { get; set; }
        public string City { get; set; }
        public string Country { get; set; }

        public void CreateMappings(Profile configuration)
        {
            /*
             * configuration.CreateMap<Source, Destination>();
             */

            configuration.CreateMap<User, UserForDetailedDto>();
        }
    }
}
