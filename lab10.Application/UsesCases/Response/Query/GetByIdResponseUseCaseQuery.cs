using lab10.Application.Repositories;
using MediatR;

namespace lab10.Application.UsesCases.Response.Query;

public record GetByIdResponseUseCaseQuery(Guid id) : IRequest<lab10.Domain.Entities.Response>;

internal sealed class GetByIdResponseUseCaseQryHandler(IResponseRepository repository)
    : IRequestHandler<GetByIdResponseUseCaseQuery, lab10.Domain.Entities.Response>
{
    public async Task<lab10.Domain.Entities.Response> Handle(GetByIdResponseUseCaseQuery request, CancellationToken cancellationToken)
    {
        return (await repository.GetById(request.id))!;
    }
}
