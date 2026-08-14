using Academia.Application.DTOs.User;
using Academia.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
        public async Task<ActionResult<ResponseModel>> Cadastro(RegisterModel model)
        {
            var response = await _identityService.CadastrarUsuario(model);
            return Ok(response);
        }
    }
}
