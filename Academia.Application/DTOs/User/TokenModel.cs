using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Academia.Application.DTOs.User
{
    public class TokenModel
    {
        public string? AccessToken { get; set; }
        public string? RefreshToken { get; set; }
    }
}
