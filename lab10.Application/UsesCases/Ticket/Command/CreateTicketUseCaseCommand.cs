using AutoMapper;
using lab10.Application.DTOs;
using lab10.Application.Repositories;
using MediatR;

namespace lab10.Application.UsesCases.Ticket.Command;

public record CreateTicketUseCaseCommand : IRequest<Guid>
{
    public Guid UserId { get; set; }

    public string Title { get; set; } = null!;

    public string? Description { get; set; }

    public string Status { get; set; } = null!;

};

internal sealed class CreateTicketUseCaseCmdHandler(ITIcketRepository repository, IMapper mapper)
    : IRequestHandler<CreateTicketUseCaseCommand, Guid>
{
    public async Task<Guid> Handle(CreateTicketUseCaseCommand request, CancellationToken cancellationToken)
    {
        var entity = mapper.Map<lab10.Domain.Entities.Ticket>(request);
        entity.TicketId = Guid.NewGuid();
        await repository.Add(entity);
        return entity.TicketId;
    }
}
