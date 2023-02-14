using FluentResults;
using Microsoft.AspNetCore.Mvc;
using UsuariosApi.Data.Dtos;
using UsuariosApi.Data.Request;
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
           
            if (resultado.IsFailed){
              if(resultado.Reasons[0].Message.Equals("Senha inválida")){
                return BadRequest(resultado.Reasons);
              }
              return StatusCode(500);
            } 
            return Ok(resultado.Reasons);
        }
        [HttpGet("/Ativa")]
        public IActionResult AtivaContaUsuario([FromQuery] AtivaContaRequest request)
        {
            Result result = _service.AtivaContaUsuario(request);
            if (result.IsFailed) return StatusCode(500);
            return Ok(result.Reasons);
        }
    }
}