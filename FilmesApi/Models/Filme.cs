using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace FilmesApi.Models;

public class Filme
{
    [Key]
    [Required]
    public int Id { get; set; }
    [Required(ErrorMessage = "O titulo do filme é obrigatório")]
    public string Titulo { get; set; }
    [Required(ErrorMessage = "O gênero do filme é obrigatório")]
    [MaxLength(50,ErrorMessage = "O tamanho do gênero não pode exceder 50 caracteres")]
    public string Genero { get; set; }  
    [Required]
    [Range(70,600, ErrorMessage = "A durção  deve ser entre 70 e 600 minutos")]
    public int Duracao { get; set; }
    public int ClassificacaoEtaria { get; set; }
    [JsonIgnore]
    public virtual List<Sessao> Sessoes{ get; set; }

}
