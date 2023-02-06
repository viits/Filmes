using AutoMapper;
using FilmesApi.Data;
using FilmesApi.Data.Dtos;
using FilmesApi.Models;
using FluentResults;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
namespace FilmesApi.Services;

public class FilmeService
{
    private FilmeContext _context;
    private IMapper _mapper;
    public FilmeService(FilmeContext context, IMapper mapper )
    {
        this._context = context;
        this._mapper = mapper;
    }

    public ReadFilmeDto AdicionaFilme(CreateFilmeDto filmeDto)
    {
        Filme filme = _mapper.Map<Filme>(filmeDto);
        _context.Filmes.Add(filme);
        _context.SaveChanges();
        return _mapper.Map<ReadFilmeDto>(filme);
    }

    public List<ReadFilmeDto> RecuperaFilmes(int? classificacao = null)
    {
        if (classificacao == null)
        {
            return _mapper.Map<List<ReadFilmeDto>>(_context.Filmes);
        }

        List<Filme> filmes = _context.Filmes.Where(filme => filme.ClassificacaoEtaria <= classificacao).ToList();
        if (filmes != null)
        {
            List<ReadFilmeDto> readDto = _mapper.Map<List<ReadFilmeDto>>(filmes);
            return readDto;
        }
        return null;
    }

    public ReadFilmeDto RecuperaFilmesPorId(int id)
    {
        var filme = _context.Filmes.FirstOrDefault(filme => filme.Id == id);
        if (filme == null) return null;
        var filmeDto = _mapper.Map<ReadFilmeDto>(filme);
        return filmeDto;
    }

    public Result AtualizarFilme(int id,UpdateFilmeDto filmeDto)
    {
        var filme = _context.Filmes.FirstOrDefault(filme => filme.Id == id);
        if (filme == null) return Result.Fail("Filme não encontrado");
        Filme filmes = _mapper.Map(filmeDto, filme);
        _context.Filmes.Update(filmes);
        _context.SaveChanges();
        return Result.Ok();
    }

    public Result DeleteFilme(int id)
    {
        var filme = _context.Filmes.FirstOrDefault(filme => filme.Id == id);
        if (filme != null)
        {
            _context.Filmes.Remove(filme);
            _context.SaveChanges();
            return Result.Ok();
        }
        return Result.Fail("Filme não encontrado");

    }
 /*
    public ReadFilmeDto AtualizarFilmesParcial(int id, JsonPatchDocument<UpdateFilmeDto> patch)
    {
        var filme = _context.Filmes.FirstOrDefault(filme => filme.Id == id);
        if (filme == null) return null;

        var filmeParaAtualizar = _mapper.Map<UpdateFilmeDto>(filme);
        patch.ApplyTo(filmeParaAtualizar, ModelState);
        if (!TryValidateModel(filmeParaAtualizar))
        {
            return ValidationProblem(ModelState);
        }
        _mapper.Map(filmeParaAtualizar, filme);
        _context.SaveChanges();
        return NoContent();
    }
 */
}
