using System.Text.Json;

namespace StockQuoteChecker_Inoa.Models.HgBrasil;

class HgBrasilResponse
{
    public string By { get; set; }
    public bool Valid_Key { get; set; }
    public JsonElement Results { get; set; }
    public double Execution_Time { get; set; }
    public bool From_Cache { get; set; }
}

