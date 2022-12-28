using System.ComponentModel.DataAnnotations;

namespace FilmesApi.Data.Dtos;

public class CreateEnderecoDto
{
    [Required(ErrorMessage = "O campo de logradouro é obrigatório")]
    public string Logradouro { get; set; }
    public string Bairro { get; set; }
    public int Numero { get; set; }
}
