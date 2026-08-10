using System;
using System.Collections.Generic;
using System.Text;

namespace Academia.Domain.Validation
{
    public class NotFoundException : Exception
    {
        public NotFoundException(string message): base(message) { }
    }
}
