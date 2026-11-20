using lab10.Application.Repositories;
using MediatR;

namespace lab10.Application.UsesCases.Ticket.Query;

public record GetByIdTicketUseCaseQuery(Guid id) : IRequest<lab10.Domain.Entities.Ticket>;

internal sealed class GetByIdTicketUseCaseQryHandler(ITIcketRepository repository)
    : IRequestHandler<GetByIdTicketUseCaseQuery, lab10.Domain.Entities.Ticket>
{
    public async Task<lab10.Domain.Entities.Ticket> Handle(GetByIdTicketUseCaseQuery request, CancellationToken cancellationToken)
    {
        return (await repository.GetById(request.id))!;
    }
}
