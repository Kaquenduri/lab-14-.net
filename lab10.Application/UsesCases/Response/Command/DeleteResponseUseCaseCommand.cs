using lab10.Application.Repositories;
using MediatR;

namespace lab10.Application.UsesCases.Response.Command;

public record DeleteResponseUseCaseCommand(Guid id) : IRequest<Guid>;

internal sealed class DeleteResponseUseCaseCmdHandler(IResponseRepository repository)
    : IRequestHandler<DeleteResponseUseCaseCommand, Guid>
{
    public async Task<Guid> Handle(DeleteResponseUseCaseCommand request, CancellationToken cancellationToken)
    {
        await repository.Delete(request.id);
        return request.id;
    }
}
