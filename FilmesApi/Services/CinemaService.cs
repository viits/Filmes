using AutoMapper;
using FilmesApi.Data;
using FilmesApi.Data.Dtos;
using FilmesApi.Models;
using FluentResults;

namespace FilmesApi.Services;

public class CinemaService
{
    private FilmeContext _context;
    private IMapper _mapper;

    public CinemaService(FilmeContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public ReadCinemaDto AdicionarCinema(CreateCinemaDto cinemaDto)
    {
        Cinema cinema = _mapper.Map<Cinema>(cinemaDto);
        _context.Cinemas.Add(cinema);
        _context.SaveChanges();
        return _mapper.Map<ReadCinemaDto>(cinema);
    }

    public List<ReadCinemaDto> RecuperaCinemas(string? nomeDoFilme)
    {

        if (nomeDoFilme == null)
        {
            return _mapper.Map<List<ReadCinemaDto>>(_context.Cinemas.ToList());
        }
        List<Cinema> cinemas = _context.Cinemas.ToList();
        if (cinemas == null)
        {
            return null;
        }
        if (!string.IsNullOrEmpty(nomeDoFilme))
        {
            IEnumerable<Cinema> listCinemas = from cinema in cinemas where cinema.Sessoes.Any(sessao => sessao.Filme.Titulo == nomeDoFilme) select cinema;
            cinemas = listCinemas.ToList();
        }
        List<ReadCinemaDto> readDto = _mapper.Map<List<ReadCinemaDto>>(cinemas);
        return readDto;
    }

    public ReadCinemaDto RecuperarCinemasPorId(int id)
    {
        Cinema cinema = _context.Cinemas.FirstOrDefault(cinema => cinema.Id == id);
        if (cinema != null)
        {
            ReadCinemaDto cinemaDto = _mapper.Map<ReadCinemaDto>(cinema);
            return cinemaDto;
        }
        return null;
    }

    public Result AtualizarCinemas(int id, UpdateCinemaDto cinemaDto)
    {
        Cinema cinema = _context.Cinemas.FirstOrDefault(cinema => cinema.Id == id);
        if (cinema == null)
        {
            return Result.Fail("Cinema não encontrado"); ;
        }
        Cinema cinemas = _mapper.Map(cinemaDto, cinema);
        _context.Cinemas.Update(cinemas);
        _context.SaveChanges();
        return Result.Ok();
    }

    internal Result DeleteCinema(int id)
    {
        Cinema cinema = _context.Cinemas.FirstOrDefault(cinema => cinema.Id == id);
        if (cinema == null) return Result.Fail("Cinema não encontrado");
        _context.Remove(cinema);
        _context.SaveChanges();
        return Result.Ok();
    }
}
