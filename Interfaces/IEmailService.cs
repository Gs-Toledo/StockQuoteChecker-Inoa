namespace StockQuoteChecker_Inoa.Interfaces;

public interface IEmailService
{
    Task SendAlertAsync(string subject, string body);
}
