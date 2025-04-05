namespace StockQuoteChecker_Inoa.Models;

public class Alert
{
    public string Symbol { get; set; }
    public decimal CurrentPrice { get; set; }
    public string Message { get; set; }
}

