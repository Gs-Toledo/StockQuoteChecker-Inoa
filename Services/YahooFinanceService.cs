using System.Text.Json;
using Microsoft.Extensions.Options;
using StockQuoteChecker_Inoa.Interfaces;
using StockQuoteChecker_Inoa.Models;
using StockQuoteChecker_Inoa.Models.YahooFinance;

namespace StockQuoteChecker_Inoa.Services;

public class YahooFinanceService : IStockQuoteService
{
    private readonly HttpClient _httpClient;
    private readonly YahooApiSettings _apiSettings;

    public YahooFinanceService(IOptions<YahooApiSettings> options)
    {
        _httpClient = new HttpClient();
        _httpClient.DefaultRequestHeaders.UserAgent.ParseAdd("Mozilla/5.0 (Windows NT 10.0; Win64; x64)");
        _apiSettings = options.Value;
    }

    public async Task<StockQuote?> GetQuoteAsync(string symbol)
    {
        try
        {
            var url = $"{_apiSettings.YahooFinanceUrl}/{symbol}.SA";
            Console.WriteLine($" Buscando cotação na URL: {url}");

            var response = await _httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();

            var result = JsonSerializer.Deserialize<YahooFinanceResponse>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            var quote = result?.Chart?.Result?.FirstOrDefault()?.Meta;

            if (quote == null)
            {
                Console.WriteLine("-X- Não foi possível obter a cotação do Yahoo Finance.");
                return null;
            }

            return new StockQuote
            {
                Symbol = quote.Symbol,
                Price = quote.RegularMarketPrice
            };
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro ao buscar cotações do Yahoo Finance: {ex.Message}");
            return null;
        }
    }
}