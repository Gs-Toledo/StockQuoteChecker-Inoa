namespace StockQuoteChecker_Inoa.Interfaces;

public interface IEmailService
{
    Task SendEmailAlert(string subject, string body);
}

