﻿using StockQuoteChecker_Inoa.Commands;
using StockQuoteChecker_Inoa.Interfaces;
using StockQuoteChecker_Inoa.Models;

namespace StockQuoteChecker_Inoa.Handlers;

public class CheckStockCommandHandler : ICommandHandler<CheckStockCommand>
{
    private readonly IStockQuoteService _quoteService;
    private readonly IAlertQueue _alertQueue;

    public CheckStockCommandHandler(IStockQuoteService quoteService, IAlertQueue alertQueue)
    {
        _quoteService = quoteService;
        _alertQueue = alertQueue;
    }

    public async Task HandleAsync(CheckStockCommand command, CancellationToken cancellationToken = default)
    {
        while (!cancellationToken.IsCancellationRequested)
        {
            var cotacao = await _quoteService.GetQuoteAsync(command.Symbol);

            if (cotacao != null)
            {
                Console.WriteLine($"Cotação atual de {cotacao.Symbol}: R$ {cotacao.Price}");

                if (cotacao.Price >= command.HighPrice)
                {
                    Console.WriteLine("!!! Cotação acima do limite!");
                    _alertQueue.Enqueue(new Alert
                    {
                        Symbol = command.Symbol,
                        CurrentPrice = cotacao.Price,
                        Message = $"Olá,você recebeu um alerta, pois o Ativo {command.Symbol} está acima de R$ {command.HighPrice}, Corra para Vender! o valor atual está R$ {cotacao.Price}"
                    });
                }
                else if (cotacao.Price <= command.LowPrice)
                {
                    Console.WriteLine("!!! Cotação abaixo do limite!");
                    _alertQueue.Enqueue(new Alert
                    {
                        Symbol = command.Symbol,
                        CurrentPrice = cotacao.Price,
                        Message = $"Olá,você recebeu um alerta, pois o Ativo {command.Symbol} está abaixo de R$ {command.LowPrice}, Corra para Comprar! valor atual está no precinho de R$ {cotacao.Price}"
                    });
                }
                else
                {
                    Console.WriteLine(" Cotação dentro do intervalo. :)");
                }
            }
            else
            {
                Console.WriteLine("X  Não foi possível obter a cotação. D:");
            }

            await Task.Delay(10000, cancellationToken);  // Para não exaurir meus tokens
        }
    }
}