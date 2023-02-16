using AutoMapper;
using FilmesApi.Data;
using FilmesApi.Data.Dtos;
using FilmesApi.Models;
using FilmesApi.Services;
using FluentResults;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace FilmesApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FilmeController : ControllerBase
    {

        private readonly FilmeService _service;
        public FilmeController(FilmeService service)
        {
            this._service = service;
        }

        [HttpPost]   
        [Authorize(Roles = "admin")]
        public IActionResult AdicionarFilme([FromBody] CreateFilmeDto filmeDto) {

           ReadFilmeDto filme = _service.AdicionaFilme(filmeDto);

           
            return CreatedAtAction(nameof(RecuperarPorId), new { id = filme.Id },filme);
        }
        
        [HttpGet]
        [Authorize(Roles = "admin, regular", Policy = "IdadeMinima")]
        public IActionResult RecuperaFilmes([FromQuery] int? classificacao = null)
        {   
            List<ReadFilmeDto> filmeDto = _service.RecuperaFilmes(classificacao);
            if(filmeDto!= null)
            {
                return Ok(filmeDto);
            }
            return NotFound();
        }
        //public IEnumerable<ReadFilmeDto> RecuperaFilmes([FromQuery] int skip = 0, [FromQuery] int take = 2) {
          //  return _mapper.Map<List<ReadFilmeDto>>(_context.Filmes.Skip(skip).Take(take));
        //}

        [HttpGet("{id}")]
        public IActionResult RecuperarPorId(int id) {
            ReadFilmeDto filmeDto = _service.RecuperaFilmesPorId(id);
            if (filmeDto == null) return NotFound();
            return Ok(filmeDto);
        }
        [HttpPut("{id}")]
        public IActionResult AtualizarFilme(int id, [FromBody] UpdateFilmeDto filmeDto) {
            Result resultado = _service.AtualizarFilme(id, filmeDto);
            if (resultado.IsFailed) return NotFound();
            return NoContent();
        }
        /*[HttpPatch("{id}")]
        public IActionResult AtualizarFilmesParcial(int id, JsonPatchDocument<UpdateFilmeDto> patch)
        {
            var filme = _context.Filmes.FirstOrDefault(filme => filme.Id == id);
            if (filme == null) return NotFound();

            var filmeParaAtualizar = _mapper.Map<UpdateFilmeDto>(filme);
            patch.ApplyTo(filmeParaAtualizar, ModelState);
            if (!TryValidateModel(filmeParaAtualizar))
            {
                return ValidationProblem(ModelState);
            }
            _mapper.Map(filmeParaAtualizar, filme);
            _context.SaveChanges();
            return NoContent();
        }*/

        [HttpDelete("{id}")]
        public IActionResult DeleteFilme(int id)
        {
            Result resultado = _service.DeleteFilme(id);
            if (resultado.IsFailed) return NotFound();
            return NoContent();
        }
    }
}
