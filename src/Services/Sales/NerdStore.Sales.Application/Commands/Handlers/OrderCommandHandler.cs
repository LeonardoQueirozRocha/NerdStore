using MediatR;

namespace NerdStore.Sales.Application.Commands.Handlers;

public class OrderCommandHandler : IRequestHandler<AddOrderItemCommand, bool>
{
    public async Task<bool> Handle(AddOrderItemCommand message, CancellationToken cancellationToken)
    {
        if (!ValidateCommand(message)) return false;

        return true;
    }

    private static bool ValidateCommand(AddOrderItemCommand message)
    {
        if (message.IsValid()) return true;

        foreach (var error in message.ValidationResult.Errors)
        {
            // throw an error event
        }

        return false;
    }
}