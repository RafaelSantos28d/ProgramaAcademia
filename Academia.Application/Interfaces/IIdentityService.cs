using Academia.Application.DTOs.User;
using System;
using System.Collections.Generic;
using System.Text;

namespace Academia.Application.Interfaces
{
    public interface IIdentityservice
    {
        Task<ResponseModel> CadastrarUsuario(RegisterModel register);
        Task<TokenModel> Login(LoginModel login);

    }
}
