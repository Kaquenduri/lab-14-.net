using AutoMapper;
using lab10.Application.DTOs;
using lab10.Application.Repositories;
using MediatR;

namespace lab10.Application.UsesCases.Ticket.Command;

public record UpdateTicketUseCaseCommand : IRequest<Guid>
{
    public Guid TicketId { get; set; }

    public Guid UserId { get; set; }

    public string Title { get; set; } = null!;

    public string? Description { get; set; }

    public string Status { get; set; } = null!;
};

internal sealed class UpdateTicketUseCaseCmdHandler(ITIcketRepository repository, IMapper mapper)
    : IRequestHandler<UpdateTicketUseCaseCommand, Guid>
{
    public async Task<Guid> Handle(UpdateTicketUseCaseCommand request, CancellationToken cancellationToken)
    {
        var entity = await repository.GetById(request.TicketId)
            ?? throw new KeyNotFoundException("Ticket not found.");
        mapper.Map(request, entity);
        await repository.Update(entity);
        return entity.TicketId;
    }
}
