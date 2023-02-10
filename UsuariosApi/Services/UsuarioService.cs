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
    public UsuarioService(UserDbContext context, IMapper mapper, UserManager<IdentityUser<int>> userManager, EmailService emailService)
    {
        _context = context;
        _mapper = mapper;
        _userManager = userManager;
        _emailService = emailService;
    }

    public Result CadastrarUsuario(CreateUsuarioDto createDto)
    {
        Usuario usuario = _mapper.Map<Usuario>(createDto);
        IdentityUser<int> usuarioIdentity = _mapper.Map<IdentityUser<int>>(usuario);
        Task<IdentityResult> resultadoIdentity = _userManager.CreateAsync(usuarioIdentity, createDto.Password);
        if (resultadoIdentity.Result.Succeeded)
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
        return Result.Fail("Falha ao cadastrar um Usuário");
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
