using AutoMapper;
using FluentResults;
using Microsoft.AspNetCore.Identity;
using UsuariosApi.Data;
using UsuariosApi.Data.Dtos;
using UsuariosApi.Models;

namespace UsuariosApi.Services;

public class UsuarioService
{
    private UserDbContext _context;
    private IMapper _mapper;
    private UserManager<IdentityUser<int>> _userManager;
    public UsuarioService(UserDbContext context, IMapper mapper, UserManager<IdentityUser<int>> userManager)
    {   
        _context = context;
        _mapper = mapper;
        _userManager = userManager;
    }

    public Result CadastrarUsuario(CreateUsuarioDto createDto)
    {
        Usuario usuario = _mapper.Map<Usuario>(createDto);
        IdentityUser<int> usuarioIdentity = _mapper.Map<IdentityUser<int>>(usuario);
        var resultadoIdentity = _userManager.CreateAsync(usuarioIdentity);
        if (resultadoIdentity.Result.Succeeded) return Result.Ok();
        return Result.Fail("Falha ao cadastrar um Usuário");
    }
}
