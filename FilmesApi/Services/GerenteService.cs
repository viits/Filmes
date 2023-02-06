using AutoMapper;
using FilmesApi.Data;
using FilmesApi.Data.Dtos;
using FilmesApi.Models;
using FluentResults;

namespace FilmesApi.Services
{
    public class GerenteService
    {
        private readonly FilmeContext _context;
        private readonly IMapper _mapper;
        public GerenteService(FilmeContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public ReadGerenteDto AdicionarGerente(CreateGerenteDto gerenteDto)
        {
            Gerente gerente = _mapper.Map<Gerente>(gerenteDto);
            _context.Gerentes.Add(gerente);
            _context.SaveChanges();
            return _mapper.Map<ReadGerenteDto>(gerente);
        }
            
        public ReadGerenteDto RecuperarPorId(int id)
        {
            var gerente = _context.Gerentes.FirstOrDefault(gerente => gerente.Id == id);
            if (gerente == null) return null;
            var gerenteDto = _mapper.Map<ReadGerenteDto>(gerente);
            return gerenteDto;
        }

        public Result DeletaGerente(int id)
        {
            Gerente gerente = _context.Gerentes.FirstOrDefault(gerente => gerente.Id == id);
            if (gerente == null) return Result.Fail("Gerente não encontrado");
            _context.Gerentes.Remove(gerente);
            _context.SaveChanges();
            return Result.Ok();
        }

    }
}
