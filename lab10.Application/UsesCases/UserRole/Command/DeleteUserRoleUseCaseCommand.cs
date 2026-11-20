using lab10.Application.DTOs;
using lab10.Application.Repositories;
using MediatR;

namespace lab10.Application.UsesCases.UserRole.Command;

public record DeleteUserRoleUseCaseCommand(Guid userId, Guid roleId) : IRequest<UserRoleKey>;

internal sealed class DeleteUserRoleUseCaseCmdHandler(IUserRoleRepository repository)
    : IRequestHandler<DeleteUserRoleUseCaseCommand, UserRoleKey>
{
    public async Task<UserRoleKey> Handle(DeleteUserRoleUseCaseCommand request, CancellationToken cancellationToken)
    {
        await repository.Delete(request.userId, request.roleId);
        return new UserRoleKey(request.userId, request.roleId);
    }
}
