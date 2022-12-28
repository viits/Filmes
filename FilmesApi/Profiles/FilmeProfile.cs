using AutoMapper;
using FilmesApi.Data.Dtos.Filme;
using FilmesApi.Models;

namespace FilmesApi.Profiles;

public class FilmeProfile : Profile
{
    public FilmeProfile()
    {
        CreateMap<CreateFilmeDto, Filme>();
        CreateMap<UpdateFilmeDto, Filme>();
        CreateMap<Filme, ReadFilmeDto>();
        CreateMap<Filme,UpdateFilmeDto > ();
    }
}
