using System.Net;
using System.Net.Mail;
using BankRadar.Models;

namespace BankRadar.Services
{
    public class EmailService
    {
        private const string EmailRemetente = "SEU_EMAIL_AQUI@gmail.com";
        private const string SenhaApp = "SUA_SENHA_AQUI";
        private const string EmailDestinatario = "EMAIL_DESTINO_AQUI@gmail.com";

        public void EnviarAlerta(List<Simulacao> taxasBaixas)
        {
            var smtp = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                Credentials = new NetworkCredential(EmailRemetente, SenhaApp),
                EnableSsl = true
            };

            var corpo = "🚨 BankRadar - Taxas abaixo do limite!\n\n";
            foreach (var s in taxasBaixas)
            {
                corpo += $"{s.Banco} | {s.TaxaMensal}% a.m. | {s.TaxaAnual}% a.a.\n";
            }

            var mensagem = new MailMessage(EmailRemetente, EmailDestinatario)
            {
                Subject = "🤖 BankRadar - Alerta de Taxa Baixa!",
                Body = corpo
            };

            smtp.Send(mensagem);
            Console.WriteLine("📧 E-mail de alerta enviado!");
        }
    }
}