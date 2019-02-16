using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace RpMan.Application.Exceptions
{
    public class IdentityErrorException : GenericException
    {
        public IdentityErrorException(string message, IEnumerable<IdentityError> failures) 
            : base(message, failures) { }
    }
}