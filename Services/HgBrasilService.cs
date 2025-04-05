using System.Text.Json;
using Microsoft.Extensions.Options;
using StockQuoteChecker_Inoa.Interfaces;
using StockQuoteChecker_Inoa.Models;


namespace StockQuoteChecker_Inoa.Services;

class HgBrasilService : IStockQuoteService
{
    private readonly HttpClient _httpClient;
    private readonly HgApiSettings _apiSettings;

    public HgBrasilService(IOptions<HgApiSettings> options)
    {
        _httpClient = new HttpClient();
        _apiSettings = options.Value;
    }

    public async Task<StockQuote>? GetQuoteAsync(string symbol)
    {
        try
        {
            string url = $"{_apiSettings.FinanceApiUrl}?symbol={symbol}&key={_apiSettings.HgBrasilApiKey}";
            var response = await _httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();



            var result = JsonSerializer.Deserialize<HgBrasilResponse>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });


            if (result?.Results.TryGetProperty("error", out var errorElement) == true && errorElement.GetBoolean())
            {
                string message = result.Results.GetProperty("message").GetString();
                Console.WriteLine($"Erro da API HG Brasil: {message}");
                return null;
            }


            var quotesDict = JsonSerializer.Deserialize<Dictionary<string, HgBrasilQuote>>(result.Results.GetRawText());
            var quote = quotesDict?.Values.FirstOrDefault();

            return new StockQuote
            {
                Symbol = quote.Symbol,
                Price = (decimal)quote.Price
            };


            return null;
        }
        catch (Exception exeption)
        {
            Console.WriteLine($"Erro ao buscar cotações: {exeption.Message}");
            return null;
        }
    }

}

