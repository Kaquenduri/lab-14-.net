using lab10.Application.Repositories;
using MediatR;

namespace lab10.Application.UsesCases.Role.Query;

public record GetByIdRoleUseCaseQuery(Guid id) : IRequest<lab10.Domain.Entities.Role>;

internal sealed class GetByIdRoleUseCaseQryHandler(IRoleRepository repository)
    : IRequestHandler<GetByIdRoleUseCaseQuery, lab10.Domain.Entities.Role>
{
    public async Task<lab10.Domain.Entities.Role> Handle(GetByIdRoleUseCaseQuery request, CancellationToken cancellationToken)
    {
        return (await repository.GetById(request.id))!;
    }
}
