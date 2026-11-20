using lab10.Application.Repositories;
using MediatR;

namespace lab10.Application.UsesCases.Role.Query;

public record GetAllRoleUseCaseQuery() : IRequest<IEnumerable<lab10.Domain.Entities.Role>>;

internal sealed class GetAllRoleUseCaseQryHandler(IRoleRepository repository)
    : IRequestHandler<GetAllRoleUseCaseQuery, IEnumerable<lab10.Domain.Entities.Role>>
{
    public async Task<IEnumerable<lab10.Domain.Entities.Role>> Handle(GetAllRoleUseCaseQuery request, CancellationToken cancellationToken)
    {
        return (await repository.GetAll());
    }
}
