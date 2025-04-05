using System.Text.Json.Serialization;

namespace StockQuoteChecker_Inoa.Models.HgBrasil;

class HgBrasilQuote
{
    [JsonPropertyName("symbol")]
    public string Symbol { get; set; }

    [JsonPropertyName("price")]
    public double Price { get; set; }
}

