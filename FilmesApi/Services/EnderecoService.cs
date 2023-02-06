using AutoMapper;
using FilmesApi.Data;
using FilmesApi.Data.Dtos;
using FilmesApi.Models;
using FluentResults;

namespace FilmesApi.Services
{
    public class EnderecoService
    {

        private readonly FilmeContext _context;
        private readonly IMapper _mapper;

        public EnderecoService(FilmeContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public ReadEnderecoDto AdicionarEndereco(CreateEnderecoDto enderecoDto)
        {
            Endereco endereco = _mapper.Map<Endereco>(enderecoDto);
            _context.Enderecos.Add(endereco);
            _context.SaveChanges();
            return _mapper.Map<ReadEnderecoDto>(endereco);
        }

        public IEnumerable<ReadEnderecoDto> RecuperarEndereco(int skip = 0, int take = 2)
        {
            return _mapper.Map<List<ReadEnderecoDto>>(_context.Enderecos.Skip(skip).Take(take));
        }
        public ReadEnderecoDto RecuperarPorId(int id)
        {
            var endereco = _context.Enderecos.FirstOrDefault(endereco => endereco.Id == id);
            if (endereco == null) return null;
            var enderecoDto = _mapper.Map<ReadEnderecoDto>(endereco);
            return enderecoDto;
        }

        public Result AtualizarEndereco(int id, UpdateEnderecoDto enderecoDto)
        {
            var endereco = _context.Enderecos.FirstOrDefault(endereco => endereco.Id == id);
            if (endereco == null) return Result.Fail("Endereço não encontrado");
            _mapper.Map(enderecoDto, endereco);
            _context.SaveChanges();
            return Result.Ok();
        }
        public Result DeletarEndereco(int id)
        {
            var endereco = _context.Enderecos.FirstOrDefault(endereco => endereco.Id == id);
            if (endereco == null) return Result.Fail("Endereço não encontrado");
            _context.Enderecos.Remove(endereco);
            _context.SaveChanges();
            return Result.Ok();
        }
    }
}
