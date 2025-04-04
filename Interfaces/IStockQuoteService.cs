using StockQuoteChecker_Inoa.Models;

namespace StockQuoteChecker_Inoa.Interfaces;

public interface IStockQuoteService
{
    Task<StockQuote> GetQuoteAsync(string symbol);
}

