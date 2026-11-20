using lab10.Application.Repositories;
using MediatR;

namespace lab10.Application.UsesCases.UserRole.Query;

public record GetAllUserRoleUseCaseQuery() : IRequest<IEnumerable<lab10.Domain.Entities.UserRole>>;

internal sealed class GetAllUserRoleUseCaseQryHandler(IUserRoleRepository repository)
    : IRequestHandler<GetAllUserRoleUseCaseQuery, IEnumerable<lab10.Domain.Entities.UserRole>>
{
    public async Task<IEnumerable<lab10.Domain.Entities.UserRole>> Handle(GetAllUserRoleUseCaseQuery request, CancellationToken cancellationToken)
    {
        return (await repository.GetAll());
    }
}
