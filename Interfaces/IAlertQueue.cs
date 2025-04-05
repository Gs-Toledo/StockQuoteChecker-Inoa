using StockQuoteChecker_Inoa.Models;

namespace StockQuoteChecker_Inoa.Interfaces;

public interface IAlertQueue
{
    void Enqueue(Alert alert);
    Alert Dequeue();
}

