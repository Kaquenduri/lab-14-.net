using lab10.Application.Repositories;
using MediatR;

namespace lab10.Application.UsesCases.UserEntity.Command;

public record DeleteUserUseCaseCommand(Guid id): IRequest<Guid>
{
    
}

//Handler

internal sealed class DeleteUserUseCaseCmdHandler : IRequestHandler<DeleteUserUseCaseCommand, Guid>
{
    private readonly IUserRepository _userRepository;

    public DeleteUserUseCaseCmdHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<Guid> Handle(DeleteUserUseCaseCommand request, CancellationToken cancellationToken)
    {
        await _userRepository.Delete(request.id);
        return request.id;
    }
}