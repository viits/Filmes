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
    public class CinemaController : ControllerBase
    {
        private readonly CinemaService _service;

        public CinemaController(CinemaService service)
        {
            _service = service;
        }
        [HttpPost]
        public IActionResult AdicionaCinema([FromBody] CreateCinemaDto cinemaDto)
        {
          ReadCinemaDto readDto = _service.AdicionarCinema(cinemaDto);

            return CreatedAtAction(nameof(RecuperaCinemasPorId), new { Id = readDto.Id }, readDto);
        }

        [HttpGet]
        public IActionResult RecuperaCinemas([FromQuery] string? nomeDoFilme = null)
        {

            List<ReadCinemaDto> listDto = _service.RecuperaCinemas(nomeDoFilme);
            if(listDto == null) return NotFound();
            return Ok(listDto);
            
            //return _context.Cinemas;
        }

        [HttpGet("{id}")]
        public IActionResult RecuperaCinemasPorId(int id)
        {
            ReadCinemaDto readDto = _service.RecuperarCinemasPorId(id);
            if(readDto == null) return NotFound();
            return Ok(readDto);
     
        }

        [HttpPut("{id}")]
        public IActionResult AtualizaCinema(int id, [FromBody] UpdateCinemaDto cinemaDto)
        {
            Result resultado= _service.AtualizarCinemas(id, cinemaDto);
            if (resultado.IsFailed) return NotFound();
            return NoContent();
           
        }


        [HttpDelete("{id}")]
        public IActionResult DeletaCinema(int id)
        {
            Result resultado = _service.DeleteCinema(id);
            if (resultado.IsFailed) return NotFound();
            return NoContent();
            
        }

    }
}
