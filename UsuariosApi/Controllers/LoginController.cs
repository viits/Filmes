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
            if (result.Result.IsFailed) return Unauthorized(result.Result.Errors);
            return Ok(result.Result.Successes);
        }

    }
}
