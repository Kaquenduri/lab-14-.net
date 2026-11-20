using lab10.Application.Repositories;
using MediatR;

namespace lab10.Application.UsesCases.Ticket.Query;

public record GetAllTicketUseCaseQuery() : IRequest<IEnumerable<lab10.Domain.Entities.Ticket>>;

internal sealed class GetAllTicketUseCaseQryHandler(ITIcketRepository repository)
    : IRequestHandler<GetAllTicketUseCaseQuery, IEnumerable<lab10.Domain.Entities.Ticket>>
{
    public async Task<IEnumerable<lab10.Domain.Entities.Ticket>> Handle(GetAllTicketUseCaseQuery request, CancellationToken cancellationToken)
    {
        return (await repository.GetAll());
    }
}
