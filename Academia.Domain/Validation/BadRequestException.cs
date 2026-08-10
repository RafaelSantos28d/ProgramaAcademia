using System;
using System.Collections.Generic;
using System.Text;

namespace Academia.Domain.Validation
{
    public class BadRequestException :Exception
    {
        public BadRequestException(string message) : base(message) { }
    }
}
