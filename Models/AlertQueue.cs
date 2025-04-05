using StockQuoteChecker_Inoa.Interfaces;

namespace StockQuoteChecker_Inoa.Models;

public class AlertQueue : IAlertQueue
{
    private readonly Queue<Alert> _alerts = new();

    public void Enqueue(Alert alert)
    {
        lock (_alerts) { _alerts.Enqueue(alert); }
    }

    public Alert Dequeue()
    {
        lock (_alerts)
        {
            if (_alerts.Count == 0) return null;
            return _alerts.Dequeue();
        }
    }
}
