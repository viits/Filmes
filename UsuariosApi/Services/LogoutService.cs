using FluentResults;
using Microsoft.AspNetCore.Identity;
using UsuariosApi.Models;

namespace UsuariosApi.Services;
public class LogoutService
{
    private SignInManager<CustomIdentityUser> _signinManager;
    public LogoutService(SignInManager<CustomIdentityUser> signinManager)
    {
        _signinManager = signinManager;
    }
    public async Task<Result> DeslogaUsuario(){
        var resultadoIdentity =  _signinManager.SignOutAsync();
        if(resultadoIdentity.IsCompletedSuccessfully) return Result.Ok();
        return Result.Fail("Logout falhou");
    }
}