namespace StockQuoteChecker_Inoa.Interfaces;


public interface ICommandHandler
{
    Task ExecuteAsync(string symbol, decimal sellPrice, decimal buyPrice, CancellationToken token);
}


