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
    private UserManager<IdentityUser<int>> _userManager;
    private readonly EmailService _emailService;
    private readonly RoleManager<IdentityRole<int>> _roleManager;
    public UsuarioService(UserDbContext context, IMapper mapper, UserManager<IdentityUser<int>> userManager, EmailService emailService, RoleManager<IdentityRole<int>> roleManager)
    {
        _context = context;
        _mapper = mapper;
        _userManager = userManager;
        _emailService = emailService;
        _roleManager = roleManager;
    }

    public async Task<Result> CadastrarUsuario(CreateUsuarioDto createDto)
    {
        Usuario usuario = _mapper.Map<Usuario>(createDto);
        IdentityUser<int> usuarioIdentity = _mapper.Map<IdentityUser<int>>(usuario);
        try
        {
            IdentityResult resultadoIdentity = await _userManager.CreateAsync(usuarioIdentity, createDto.Password);
            
            var createRoleResult = _roleManager.CreateAsync(new IdentityRole<int>("admin")).Result;
            var usuarioRoleResult = _userManager.AddToRoleAsync(usuarioIdentity, "admin").Result;

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
