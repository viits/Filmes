using FluentResults;
using Microsoft.AspNetCore.Mvc;
using UsuariosApi.Data.Request;
using UsuariosApi.Services;

namespace UsuariosApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LoginController : ControllerBase
    {
        private LoginService _service;
        public LoginController(LoginService service)
        {
            _service = service;
        }
        [HttpPost]
        public IActionResult LoginUsuario(LoginRequest request)
        {
            Task<Result> result = _service.LogaUsuario(request);
            if (result.Result.IsFailed) return Unauthorized(result.Result.Reasons);
            return Ok(result.Result.Reasons);
        }
        [HttpPost("solicita-senha")]
        public IActionResult SolicitaResetSenha(SolicitaResetRequest request){
            Result resultado = _service.SolicitaSenha(request);
            if(resultado.IsFailed) return Unauthorized(resultado.Reasons);
            return Ok(resultado.Reasons);
        }

         [HttpPost("efetua-reset-senha")]
        public IActionResult ResetSenhaUsuario(EfetuaResetRequest request){
            Result resultado = _service.ResetaSenha(request);
            if(resultado.IsFailed) return Unauthorized(resultado.Reasons);
            return Ok(resultado.Reasons);
        }
    }
}
