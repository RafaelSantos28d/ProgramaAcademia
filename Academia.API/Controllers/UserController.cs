using Academia.Application.DTOs.User;
using Academia.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Academia.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    
    
    public class UserController : ControllerBase
    {
        private readonly IIdentityservice _identityService;

        public UserController(IIdentityservice identityService)
        {
            _identityService = identityService;
        }
        [HttpPost]
        public async Task<ActionResult<TokenModel>>Login(LoginModel model)
        {
            var token = await _identityService.Login(model);
            return Ok(token);
        }
        [HttpPost("Cadastro")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<ResponseModel>> Cadastro(RegisterModel model)
        {
            var response = await _identityService.CadastrarUsuario(model);
            return Ok(response);
        }
        [HttpPost("CreateRole")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<ResponseModel>> CreateRole(string roleName)
        {
            var role = await _identityService.CreateRole(roleName);
            return Ok(role);
        }
        [HttpPost("AddRoleToUser")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<ResponseModel>> AddRoleToUser(string email, string roleName)
        {
            var result = await _identityService.AddRoleToUser(email, roleName);
            return Ok(result);
        }
        
    }
}
