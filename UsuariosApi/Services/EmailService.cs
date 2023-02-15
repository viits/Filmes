using FluentResults;
using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Identity;
using MimeKit;
using UsuariosApi.Models;

namespace UsuariosApi.Services;
public class EmailService
{

    private IConfiguration _configuration;
    public EmailService(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    public Result enviarEmail(List<IdentityUser<int>> destinatario, string assunto, int usuarioId, string token)
    {
        Mensagem mensagem = new Mensagem(destinatario, assunto, usuarioId, token);
        var mensagemEmail = CriaCorpoEmail(mensagem);
        Enviar(mensagemEmail);
        return Result.Ok();
    }
    public Result enviarEmailResetPassword(List<IdentityUser<int>> destinatario, string assunto, string token)
    {
        Mensagem mensagem = new Mensagem(destinatario, assunto, token);
        var mensagemEmail = CriaCorpoEmail(mensagem);
        Enviar(mensagemEmail);
        return Result.Ok();
    }
    private MimeMessage CriaCorpoEmail(Mensagem mensagem)
    {
        var mensagemEmail = new MimeMessage();
        mensagemEmail.From.Add(new MailboxAddress(_configuration.GetValue<string>("EmailSettings:Name"), _configuration.GetValue<string>("EmailSettings:From")));
        mensagemEmail.To.AddRange(mensagem.Destinatario);
        mensagemEmail.Subject = mensagem.Assunto;
        mensagemEmail.Body = new TextPart(MimeKit.Text.TextFormat.Text)
        {
            Text = mensagem.Conteudo
        };
        return mensagemEmail;
    }
    private void Enviar(MimeMessage mensagem)
    {
        using (var client = new SmtpClient())
        {
            try
            {
                client.Connect(_configuration.GetValue<string>("EmailSettings:SmtpServer"),
                    _configuration.GetValue<int>("EmailSettings:Port"),
                    MailKit.Security.SecureSocketOptions.StartTls
                  );
                client.AuthenticationMechanisms.Remove("XOUATH2");
                client.Authenticate(_configuration.GetValue<string>("EmailSettings:From"),
                _configuration.GetValue<string>("EmailSettings:Password")
                );
                client.Send(mensagem);
            }
            catch (Exception e)
            {
                throw;
            }
            finally
            {
                client.Disconnect(true);
                client.Dispose();
            }
        };
    }
}