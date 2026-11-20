using AutoMapper;
using lab10.Application.DTOs;
using lab10.Application.Repositories;
using MediatR;

namespace lab10.Application.UsesCases.Response.Command;

public record CreateResponseUseCaseCommand() : IRequest<Guid>
{
    public Guid TicketId { get; set; }

    public Guid ResponderId { get; set; }

    public string Message { get; set; } = null!;
};

//Handler 
internal sealed class CreateResponseUseCaseCommandHandler(IResponseRepository repository, IMapper mapper)
    : IRequestHandler<CreateResponseUseCaseCommand, Guid>
{
    public async Task<Guid> Handle(CreateResponseUseCaseCommand request, CancellationToken cancellationToken)
    {
        var entity = mapper.Map<lab10.Domain.Entities.Response>(request);
        entity.ResponseId = Guid.NewGuid();
        await repository.Add(entity);
        return entity.ResponseId;
    }
}
