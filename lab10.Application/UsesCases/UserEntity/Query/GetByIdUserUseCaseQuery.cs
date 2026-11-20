using lab10.Application.Repositories;
using lab10.Domain.Entities;
using MediatR;

namespace lab10.Application.UsesCases.UserEntity.Query;

public record GetByIdUserUseCaseQuery (Guid id) : IRequest<User>; 

//Handler 
internal sealed class GetByIdUserUseCaseQryHandler : IRequestHandler<GetByIdUserUseCaseQuery, User>
{
    private readonly IUserRepository _userRepository;

    public GetByIdUserUseCaseQryHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<User> Handle(GetByIdUserUseCaseQuery request, CancellationToken cancellationToken)
    {
        var entity = await _userRepository.GetById(request.id);
        return entity!;
    }
}