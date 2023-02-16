using Microsoft.AspNetCore.Identity;

namespace UsuariosApi.Models;
public class CustomIdentityUser: IdentityUser<int>
{
    public DateTime DataNascimento { get; set; }

}