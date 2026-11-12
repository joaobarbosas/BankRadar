using System.Net;
using System.Net.Mail;
using BankRadar.Models;

namespace BankRadar.Services;

public class EmailService
{
    public void EnviarAlerta(List<Simulacao> taxasBaixas)
    {
        var sender = GetRequired("BANKRADAR_SMTP_SENDER");
        var password = GetRequired("BANKRADAR_SMTP_PASSWORD");
        var recipient = GetRequired("BANKRADAR_SMTP_RECIPIENT");
        var host = Environment.GetEnvironmentVariable("BANKRADAR_SMTP_HOST") ?? "smtp.gmail.com";
        var port = int.TryParse(Environment.GetEnvironmentVariable("BANKRADAR_SMTP_PORT"), out var configuredPort)
            ? configuredPort : 587;

        using var smtp = new SmtpClient(host)
        {
            Port = port,
            Credentials = new NetworkCredential(sender, password),
            EnableSsl = true
        };

        var body = "BankRadar - Rates below threshold!\n\n" +
            string.Join("\n", taxasBaixas.Select(s => $"{s.Banco} | {s.TaxaMensal}% monthly | {s.TaxaAnual}% yearly"));

        using var message = new MailMessage(sender, recipient)
        {
            Subject = "BankRadar - Low rate alert",
            Body = body
        };

        smtp.Send(message);
        Console.WriteLine("Email alert sent.");
    }

    private static string GetRequired(string name) =>
        Environment.GetEnvironmentVariable(name) is { Length: > 0 } value
            ? value
            : throw new InvalidOperationException($"Set {name} before sending email alerts.");
}
