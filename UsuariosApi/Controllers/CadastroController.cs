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
            Task<Result> resultado = _service.CadastrarUsuario(createDto);
           
            if (resultado.Result.IsFailed){
              if(resultado.Result.Reasons.Count > 0){
                return BadRequest(resultado.Result.Reasons);
              }
              return StatusCode(500);
            } 
            return Ok(resultado.Result.Reasons);
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