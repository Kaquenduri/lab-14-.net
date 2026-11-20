using lab10.Application.Repositories;
using MediatR;

namespace lab10.Application.UsesCases.Ticket.Command;

public record DeleteTicketUseCaseCommand(Guid id) : IRequest<Guid>;

internal sealed class DeleteTicketUseCaseCmdHandler(ITIcketRepository repository)
    : IRequestHandler<DeleteTicketUseCaseCommand, Guid>
{
    public async Task<Guid> Handle(DeleteTicketUseCaseCommand request, CancellationToken cancellationToken)
    {
        await repository.Delete(request.id);
        return request.id;
    }
}
