using lab10.Application.Repositories;
using MediatR;

namespace lab10.Application.UsesCases.Role.Command;

public record DeleteRoleUseCaseCommand(Guid id) : IRequest<Guid>;

internal sealed class DeleteRoleUseCaseCmdHandler(IRoleRepository repository)
    : IRequestHandler<DeleteRoleUseCaseCommand, Guid>
{
    public async Task<Guid> Handle(DeleteRoleUseCaseCommand request, CancellationToken cancellationToken)
    {
        await repository.Delete(request.id);
        return request.id;
    }
}
