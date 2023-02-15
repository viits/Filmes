using FluentResults;
using Microsoft.AspNetCore.Identity;
using UsuariosApi.Data.Request;
using UsuariosApi.Models;

namespace UsuariosApi.Services;

public class LoginService
{
    private readonly SignInManager<IdentityUser<int>> _signInManager;
    private readonly TokenService _tokenService;
    private readonly EmailService _emailService;
    public LoginService(SignInManager<IdentityUser<int>> signInManager, TokenService tokenService, EmailService emailService)
    {
        _signInManager = signInManager;
        _tokenService = tokenService;
        _emailService = emailService;
    }
    public async Task<Result> LogaUsuario(LoginRequest request)
    {

        var resultadoIdentity = await _signInManager.PasswordSignInAsync(request.UserName, request.Password, false, false);
        if (resultadoIdentity.Succeeded)
        {
            var identityUser = _signInManager.UserManager.Users.FirstOrDefault(usuario => usuario.NormalizedUserName == request.UserName.ToUpper());
            Token token = _tokenService.CreateToken(identityUser,_signInManager.UserManager.GetRolesAsync(identityUser).Result.FirstOrDefault());
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
        _emailService.enviarEmailResetPassword(
            new List<IdentityUser<int>> { identityUser },
            "Link Para resetar a Senha",
            codigoRecuperacao
        );
        return Result.Ok().WithSuccess("Email enviado com sucesso!");
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
