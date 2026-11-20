using AutoMapper;
using lab10.Application.DTOs;
using lab10.Application.Repositories;
using MediatR;

namespace lab10.Application.UsesCases.Response.Command;

public record UpdateResponseUseCaseCommand() : IRequest<Guid>
{
    public Guid ResponseId { get; set; }

    public Guid TicketId { get; set; }

    public Guid ResponderId { get; set; }

    public string Message { get; set; } = null!;
};

internal sealed class UpdateResponseUseCaseCommandHandler(IResponseRepository repository, IMapper mapper)
    : IRequestHandler<UpdateResponseUseCaseCommand, Guid>
{
    public async Task<Guid> Handle(UpdateResponseUseCaseCommand request, CancellationToken cancellationToken)
    {
        var entity = await repository.GetById(request.ResponseId)
            ?? throw new KeyNotFoundException("Response not found.");
        mapper.Map(request, entity);
        await repository.Update(entity);
        return entity.ResponseId;
    }
}
