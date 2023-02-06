using AutoMapper;
using FilmesApi.Data;
using FilmesApi.Data.Dtos;
using FilmesApi.Models;
using FilmesApi.Services;
using FluentResults;
using Microsoft.AspNetCore.Mvc;

namespace FilmesApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GerenteController : ControllerBase
    {
        private GerenteService _service;
        
        public GerenteController(GerenteService service)
        {
            _service = service;
           
        }
        [HttpPost]
        public IActionResult AdicionaGerente(CreateGerenteDto gerenteDto)
        {
            ReadGerenteDto dto = _service.AdicionarGerente(gerenteDto);
            return CreatedAtAction(nameof(RecuperarPorId), new { id = dto.Id }, dto);
        }

        [HttpGet("{id}")]
        public IActionResult RecuperarPorId(int id)
        {
            var gerente = _service.RecuperarPorId(id);
            if (gerente == null) return NotFound();
            return Ok(gerente);
        }
        [HttpDelete("{id}")]
        public IActionResult DeletaGerente(int id)
        {
           Result result = _service.DeletaGerente(id);
            if(result.IsFailed) return NotFound();
            return NoContent();
        }
    }
}
