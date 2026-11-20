using AutoMapper;
using lab10.Application.DTOs;
using lab10.Application.Repositories;
using lab10.Domain.Entities;
using MediatR;

namespace lab10.Application.UsesCases.UserEntity.Command;

public record UpdateUserUseCaseCommand : IRequest<Guid>
{
    public Guid UserId { get; set; }
    
    public string Username { get; set; } = null!;

    public string PasswordHash { get; set; } = null!;

    public string? Email { get; set; }
}; 

//Handler 
internal sealed class UpdateUserUseCaseCmdHandler : IRequestHandler<UpdateUserUseCaseCommand, Guid>
{
    private readonly IUserRepository _userRepository;
    private IMapper _mapper;

    public UpdateUserUseCaseCmdHandler(IUserRepository userRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper =  mapper;
    }

    public async Task<Guid> Handle(UpdateUserUseCaseCommand request, CancellationToken cancellationToken)
    {
        var entity = _mapper.Map<User>(request); 
        await _userRepository.Update(entity);
        return entity.UserId; 
    }
}