using System.Web;
using AutoMapper;
using FluentResults;
using Microsoft.AspNetCore.Identity;
using UsuariosApi.Data;
using UsuariosApi.Data.Dtos;
using UsuariosApi.Data.Request;
using UsuariosApi.Models;

namespace UsuariosApi.Services;

public class UsuarioService
{
    private UserDbContext _context;
    private IMapper _mapper;
    private UserManager<CustomIdentityUser> _userManager;
    private readonly EmailService _emailService;
    public UsuarioService(UserDbContext context, IMapper mapper, UserManager<CustomIdentityUser> userManager, EmailService emailService)
    {
        _context = context;
        _mapper = mapper;
        _userManager = userManager;
        _emailService = emailService;
    }

    public async Task<Result> CadastrarUsuario(CreateUsuarioDto createDto)
    {
        Usuario usuario = _mapper.Map<Usuario>(createDto);
        CustomIdentityUser usuarioIdentity = _mapper.Map<CustomIdentityUser>(usuario);
        try
        {
            IdentityResult resultadoIdentity = await _userManager.CreateAsync(usuarioIdentity, createDto.Password);
            await _userManager.AddToRoleAsync(usuarioIdentity, "regular");
            if (resultadoIdentity.Succeeded)
            {
                var code = _userManager.GenerateEmailConfirmationTokenAsync(usuarioIdentity).Result;

                _emailService.enviarEmail(
                    new List<IdentityUser<int>>() { usuarioIdentity },
                    "Link de Ativação",
                    usuarioIdentity.Id,
                    code
                    );
                return Result.Ok().WithSuccess(code);
            }

            return Result.Fail(resultadoIdentity.ToString());
        }
        catch (Exception e)
        {
            return Result.Fail("Falha ao cadastrar um Usuário");
        }
    }
    public Result AtivaContaUsuario(AtivaContaRequest request)
    {
        var identityUser = _userManager.Users.FirstOrDefault(usuario => usuario.Id == request.UsuarioId);
        var identityResult = _userManager.ConfirmEmailAsync(identityUser, request.CodigoDeAtivacao).Result;
        if (identityResult.Succeeded)
        {
            return Result.Ok();
        }
        return Result.Fail("Falha ao ativar a conta do Usuario");
    }
}
