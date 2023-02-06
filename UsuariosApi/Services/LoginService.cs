using FluentResults;
using Microsoft.AspNetCore.Identity;
using UsuariosApi.Data.Request;
using UsuariosApi.Models;

namespace UsuariosApi.Services;

public class LoginService
{
    private readonly SignInManager<IdentityUser<int>> _signInManager;
    private readonly TokenService _tokenService;
    public LoginService(SignInManager<IdentityUser<int>> signInManager, TokenService tokenService)
    {
        _signInManager = signInManager;
        _tokenService = tokenService;
    }
    public async Task<Result> LogaUsuario(LoginRequest request)
    {

        var resultadoIdentity = await _signInManager.PasswordSignInAsync(request.UserName, request.Password, false, false);
        if (resultadoIdentity.Succeeded)
        {
            var identityUser = _signInManager.UserManager.Users.FirstOrDefault(usuario=> usuario.NormalizedUserName == request.UserName.ToUpper());
            Token token = _tokenService.CreateToken(identityUser);
            return Result.Ok().WithSuccess(token.Value);
        }

        return Result.Fail("Usuario inválido!");
    }
}
