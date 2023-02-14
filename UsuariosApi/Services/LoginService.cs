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
            var identityUser = _signInManager.UserManager.Users.FirstOrDefault(usuario => usuario.NormalizedUserName == request.UserName.ToUpper());
            Token token = _tokenService.CreateToken(identityUser);
            return Result.Ok().WithSuccess(token.Value);
        }

        return Result.Fail("Usuario inválido!");
    }
    private IdentityUser<int> ObterUsuario(string email)
    {
        return _signInManager.UserManager.Users.FirstOrDefault(user => user.NormalizedEmail == email.ToUpper());
    }
    public Result SolicitaSenha(SolicitaResetRequest request)
    {
        IdentityUser<int> identityUser = this.ObterUsuario(request.Email);

        if (identityUser == null) return Result.Fail("E-mail não encontrado");


        string codigoRecuperacao = _signInManager.UserManager.GeneratePasswordResetTokenAsync(identityUser).Result;

        return Result.Ok().WithSuccess(codigoRecuperacao);
    }
    public Result ResetaSenha(EfetuaResetRequest request)
    {
        IdentityUser<int> identityUser = this.ObterUsuario(request.Email);
        if (identityUser == null) return Result.Fail("Email não encontrado!");

        IdentityResult resultado = _signInManager.UserManager.ResetPasswordAsync(identityUser, request.Token, request.Password).Result;
        if (resultado.Succeeded) return Result.Ok().WithSuccess("Senha Alterada com sucesso! " + resultado);
        return Result.Fail("Ocorreu um erro na alteração de Senha!");
    }
}
