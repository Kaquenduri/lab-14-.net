using lab10.Application.Repositories;
using MediatR;

namespace lab10.Application.UsesCases.UserRole.Query;

public record GetByIdUserRoleUseCaseQuery(Guid userId, Guid roleId) : IRequest<lab10.Domain.Entities.UserRole>;

internal sealed class GetByIdUserRoleUseCaseQryHandler(IUserRoleRepository repository)
    : IRequestHandler<GetByIdUserRoleUseCaseQuery, lab10.Domain.Entities.UserRole>
{
    public async Task<lab10.Domain.Entities.UserRole> Handle(GetByIdUserRoleUseCaseQuery request, CancellationToken cancellationToken)
    {
        return (await repository.GetById(request.userId, request.roleId))!;
    }
}
