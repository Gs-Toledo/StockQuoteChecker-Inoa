using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using StockQuoteChecker_Inoa.Commands;
using StockQuoteChecker_Inoa.Handlers;
using StockQuoteChecker_Inoa.Interfaces;
using StockQuoteChecker_Inoa.Models;
using StockQuoteChecker_Inoa.Models.YahooFinance;
using StockQuoteChecker_Inoa.Services;

if (args.Length != 3)
{
    Console.WriteLine("Uso correto: stock-quote-checker.exe <ATIVO> <PRECO_ALTA> <PRECO_BAIXA>");
    return;
}


var symbol = args[0];
var highPrice = decimal.Parse(args[1]);
var lowPrice = decimal.Parse(args[2]);



using IHost host = Host.CreateDefaultBuilder()
    .ConfigureAppConfiguration((context, config) =>
    {
        config.SetBasePath(Directory.GetCurrentDirectory());
        config.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
    })
    .ConfigureServices((context, services) =>
    {
        IConfiguration configuration = context.Configuration;

        services.Configure<StockAlertSettings>(configuration.GetSection("EmailSettings"));
        services.Configure<YahooApiSettings>(configuration.GetSection("ApiSettings"));


        services.AddSingleton<IEmailService, EmailService>();
        services.AddSingleton<IStockQuoteService, YahooFinanceService>();

        services.AddSingleton<IAlertQueue, AlertQueue>();
        services.AddHostedService<AlertNotificationService>();

        services.AddSingleton<ICommandHandler<CheckStockCommand>, CheckStockCommandHandler>();



    })
    .UseConsoleLifetime()
    .Build();

await host.StartAsync();

var command = new CheckStockCommand
{
    Symbol = symbol,
    HighPrice = highPrice,
    LowPrice = lowPrice
};

var handler = host.Services.GetRequiredService<ICommandHandler<CheckStockCommand>>();


_ = Task.Run(() => handler.HandleAsync(command));

await host.WaitForShutdownAsync();