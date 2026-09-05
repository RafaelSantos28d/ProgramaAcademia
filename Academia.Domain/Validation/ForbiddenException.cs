using System;
using System.Collections.Generic;
using System.Text;

namespace Academia.Domain.Validation
{
    public class ForbiddenException :Exception
    {
        public ForbiddenException(string message):base(message)
        {
            
        }
    }
}
