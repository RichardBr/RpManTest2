using System;
using System.Collections.Generic;
using System.Linq;
using FluentValidation.Results;

namespace RpMan.Application.Exceptions
{
    public class GenericException : Exception
    {
        public GenericException(string message, Object failures) : base(message)
        {
            Failures = failures;
        }

        public Object Failures { get; }
    }
}
