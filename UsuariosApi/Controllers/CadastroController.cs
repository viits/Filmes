using FluentResults;
using Microsoft.AspNetCore.Mvc;
using UsuariosApi.Data.Dtos;
using UsuariosApi.Services;

namespace UsuariosApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CadastroController : ControllerBase
    {

        private readonly UsuarioService _service;
        public CadastroController(UsuarioService service)
        {
            _service = service;
        }

        [HttpPost]
        public IActionResult CadastroUsuario(CreateUsuarioDto createDto)
        {
            Result resultado = _service.CadastrarUsuario(createDto);
            if (resultado.IsFailed) return StatusCode(500);
            return Ok();
        }

    }
}