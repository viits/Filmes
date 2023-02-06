using AutoMapper;
using FilmesApi.Data;
using FilmesApi.Data.Dtos;
using FilmesApi.Models;
using FilmesApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace FilmesApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SessaoController : ControllerBase
    {
        private SessaoService _service;
        

        public SessaoController(SessaoService service)
        {
            _service = service;
        }

        [HttpPost]
        public  IActionResult AdicionarSessao(CreateSessaoDto dto)
        {
            ReadSessaoDto sessaoDto = _service.AdicionarSessao(dto);
            return CreatedAtAction(nameof(RecuperaSessoesPorId), new { Id = sessaoDto.Id }, sessaoDto);
        }

        [HttpGet("{id}")]
        public IActionResult RecuperaSessoesPorId(int id)
        {
            var sessao = _service.RecuperarPorId(id);
            if (sessao != null)
            {
                
                return Ok(sessao);
            }
                
            return NotFound();
            
        }
    }
}
