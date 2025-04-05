namespace StockQuoteChecker_Inoa.Interfaces;

public interface ICommandHandler<TCommand>
{
    Task HandleAsync(TCommand command, CancellationToken cancellationToken = default);
}
