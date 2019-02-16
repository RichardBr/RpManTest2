using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace RpMan.Application.Exceptions
{
    public class UserLoginException : GenericException
    {
        public UserLoginException(string message) 
            : base(message, null) { }
    }
}