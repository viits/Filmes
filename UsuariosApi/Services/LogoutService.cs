using FluentResults;
using Microsoft.AspNetCore.Identity;

namespace UsuariosApi.Services;
public class LogoutService
{
    private SignInManager<IdentityUser<int>> _signinManager;
    public LogoutService(SignInManager<IdentityUser<int>> signinManager)
    {
        _signinManager = signinManager;
    }
    public async Task<Result> DeslogaUsuario(){
        var resultadoIdentity =  _signinManager.SignOutAsync();
        if(resultadoIdentity.IsCompletedSuccessfully) return Result.Ok();
        return Result.Fail("Logout falhou");
    }
}