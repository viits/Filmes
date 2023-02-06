using FluentResults;
using Microsoft.AspNetCore.Mvc;
using UsuariosApi.Services;

namespace UsuariosApi.Controllers;
[ApiController]
[Route("[controller]")]
public class LogoutController: ControllerBase
{
    private LogoutService _logoutService;
    public LogoutController(LogoutService logoutService)
    {
        _logoutService = logoutService;
    }

    [HttpPost]
    public IActionResult DeslogaUsuario(){
        Task<Result> resultado = _logoutService.DeslogaUsuario();
        if(resultado.Result.IsFailed){
            return Unauthorized(resultado.Result.Errors);
        }
        return Ok(resultado.Result.Successes);
    }

}