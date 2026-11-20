using lab10.Application.Repositories;
using lab10.Domain.Entities;
using MediatR;

namespace lab10.Application.UsesCases.UserEntity.Query;

public record GetAllUserUserCaseQuery : IRequest<IEnumerable<User>>;

//Handler 
internal sealed class GetAllUserUserCaseCmdHandler : IRequestHandler<GetAllUserUserCaseQuery, IEnumerable<User>>
{
    private readonly IUserRepository _userRepository;

    public GetAllUserUserCaseCmdHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<IEnumerable<User>> Handle(GetAllUserUserCaseQuery request, CancellationToken cancellationToken)
    {
        return await _userRepository.GetAll();
    }
}