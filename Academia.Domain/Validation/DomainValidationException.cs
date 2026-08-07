using System;
using System.Collections.Generic;
using System.Text;

namespace Academia.Domain.Validation
{
    public class DomainValidationException :Exception
    {
        public DomainValidationException(string error) : base(error)
        {
        }
        public static void When(bool hasError, string error)
        {
            if (hasError)
            {
                throw new DomainValidationException(error);
            }
        }
       

        
    }
}
