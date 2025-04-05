using Microsoft.Extensions.Hosting;
using StockQuoteChecker_Inoa.Interfaces;

namespace StockQuoteChecker_Inoa.Services;

public class AlertNotificationService : BackgroundService
{
    private readonly IAlertQueue _queue;
    private readonly IEmailService _emailService;

    public AlertNotificationService(IAlertQueue queue, IEmailService emailService)
    {
        _queue = queue;
        _emailService = emailService;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            var alert = _queue.Dequeue();

            if (alert != null)
            {
                try
                {
                    await _emailService.SendAlertAsync(
                        $"Alerta: {alert.Symbol}",
                        alert.Message
                    );
                    Console.WriteLine("Alerta enviado por email. :)");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Erro ao enviar email: {ex.Message} D:");
                }
            }

            await Task.Delay(1000, stoppingToken);
        }
    }
}