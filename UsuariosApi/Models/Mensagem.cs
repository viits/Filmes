using Microsoft.AspNetCore.Identity;
using MimeKit;

namespace UsuariosApi.Models
{
    public class Mensagem
    {
        public List<MailboxAddress> Destinatario { get; set; }
        public string Assunto { get; set; }
        public string Conteudo { get; set; }

        public Mensagem(IEnumerable<IdentityUser<int>> destinatario, string assunto, int usuarioId, string token)
        {
            Destinatario = new List<MailboxAddress>();
            Destinatario.AddRange(destinatario.Select(d => new MailboxAddress(d.UserName, d.Email)));
            Assunto = assunto;
            Conteudo = $"http://localhost:6000/ativa?usuarioId={usuarioId}&CodigoDeAtivacao={token}";
        }

        public Mensagem(IEnumerable<IdentityUser<int>> destinatario, string assunto, string token)
        {
            Destinatario = new List<MailboxAddress>();
            Destinatario.AddRange(destinatario.Select(d => new MailboxAddress(d.UserName, d.Email)));
            Assunto = assunto;
            Conteudo = $"Esse é o token para colocar na rota efetuar-reset-senha! \n Token: {token}";
        }
    }
}