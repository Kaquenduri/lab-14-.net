using AutoMapper;
using lab10.Application.DTOs;
using lab10.Application.Repositories;
using MediatR;
using Microsoft.IdentityModel.Logging;

namespace lab10.Application.UsesCases.UserEntity.Command;

public record CreateUserUseCaseCommand : IRequest<Guid>
{
    public string Username { get; set; } = null!;

    public string PasswordHash { get; set; } = null!;

    public string? Email { get; set; }
};

//Handler 

internal sealed class CreateUserUseCaseCmdHandler : IRequestHandler<CreateUserUseCaseCommand, Guid>
{
    private readonly IUserRepository _userRepository;
    private IMapper _mapper;
    
    public CreateUserUseCaseCmdHandler(IUserRepository userRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }
    
    public async Task<Guid> Handle(CreateUserUseCaseCommand request, CancellationToken cancellationToken)
    {
        var entity = _mapper.Map<Domain.Entities.User>(request);
        // Le damos un ID nuevo antes de guardar, igual que en los otros registros.
        entity.UserId = Guid.NewGuid();
        await _userRepository.Add(entity);
        return entity.UserId; 
    }
}
