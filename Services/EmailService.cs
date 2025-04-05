using System.Net;
using System.Net.Mail;
using Microsoft.Extensions.Options;
using StockQuoteChecker_Inoa.Interfaces;
using StockQuoteChecker_Inoa.Models;

namespace StockQuoteChecker_Inoa.Services;

public class EmailService : IEmailService
{
    private readonly StockAlertSettings _settings;

    public EmailService(IOptions<StockAlertSettings> options)
    {
        _settings = options.Value;
    }

    public async Task SendAlertAsync(string subject, string body)
    {
        using var client = new SmtpClient(_settings.SmtpServer, _settings.Port)
        {
            Credentials = new NetworkCredential(_settings.SenderEmail, _settings.SenderPassword),
            EnableSsl = _settings.UseSSL
        };

        var message = new MailMessage(_settings.SenderEmail, _settings.RecipientEmail, subject, body);
        await client.SendMailAsync(message);
    }
}
