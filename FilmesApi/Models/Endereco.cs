using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace FilmesApi.Models;

public class Endereco
{
    [Key]
    [Required]
    public int Id { get; set; }
    public string Logradouro { get; set; }
    public string Bairro { get; set; }
    public int Numero { get; set; }
    [JsonIgnore]
    public virtual Cinema Cinema { get; set; }
}
