using lab10.Application.Repositories;
using MediatR;

namespace lab10.Application.UsesCases.Response.Query;

public record GetAllResponseUseCaseQuery() : IRequest<IEnumerable<lab10.Domain.Entities.Response>>;

internal sealed class GetAllResponseUseCaseQryHandler(IResponseRepository repository)
    : IRequestHandler<GetAllResponseUseCaseQuery, IEnumerable<lab10.Domain.Entities.Response>>
{
    public async Task<IEnumerable<lab10.Domain.Entities.Response>> Handle(GetAllResponseUseCaseQuery request, CancellationToken cancellationToken)
    {
        return (await repository.GetAll());
    }
}
