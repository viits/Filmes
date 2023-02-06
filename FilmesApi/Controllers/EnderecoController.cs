using AutoMapper;
using FilmesApi.Data;
using FilmesApi.Data.Dtos;
using FilmesApi.Models;
using FilmesApi.Services;
using FluentResults;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace FilmesApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EnderecoController : ControllerBase
    {
        private  readonly EnderecoService _service;
          

        public EnderecoController(EnderecoService service)
        {
            _service = service;
        }

        [HttpPost]
        public IActionResult AdicionarEndereco([FromBody] CreateEnderecoDto enderecoDto)
        {

            ReadEnderecoDto dtos  = _service.AdicionarEndereco(enderecoDto);
           
            return CreatedAtAction(nameof(RecuperarPorId), new { id = dtos.Id }, dtos);
        }

        [HttpGet]
        public IEnumerable<ReadEnderecoDto> RecuperaEndereco([FromQuery] int skip = 0, [FromQuery] int take = 2)
        {
            return _service.RecuperarEndereco(skip, take);
        }

        [HttpGet("{id}")]
        public IActionResult RecuperarPorId(int id)
        {
            ReadEnderecoDto dto = _service.RecuperarPorId(id);
            if (dto == null) return NotFound();
            return Ok(dto);

        }
        [HttpPut("{id}")]
        public IActionResult AtualizarEndereco(int id, [FromBody] UpdateEnderecoDto enderecoDto)
        {
            Result result = _service.AtualizarEndereco(id, enderecoDto);
            if(result.IsFailed) return NotFound();
            return NoContent();
        }
        /*
        [HttpPatch("{id}")]
        public IActionResult AtualizarEnderecoParcial(int id, JsonPatchDocument<UpdateEnderecoDto> patch)
        {
            var endereco = _context.Enderecos.FirstOrDefault(endereco => endereco.Id == id);
            if (endereco == null) return NotFound();

            var enderecoParaAtualizar = _mapper.Map<UpdateEnderecoDto>(endereco);
            patch.ApplyTo(enderecoParaAtualizar, ModelState);
            if (!TryValidateModel(enderecoParaAtualizar))
            {
                return ValidationProblem(ModelState);
            }
            _mapper.Map(enderecoParaAtualizar, endereco);
            _context.SaveChanges();
            return NoContent();
        }
        */
        [HttpDelete("{id}")]
        public IActionResult DeleteEndereco(int id)
        {
            Result result = _service.DeletarEndereco(id);
            if (result.IsFailed) return NotFound();
            return NoContent();
        }
    }
}
