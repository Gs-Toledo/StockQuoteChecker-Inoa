namespace StockQuoteChecker_Inoa.Commands;
public class CheckStockCommand
{
    public string Symbol { get; set; } = string.Empty;
    public decimal HighPrice { get; set; }
    public decimal LowPrice { get; set; }
}
