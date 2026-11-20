using AutoMapper;
using lab10.Application.DTOs;
using lab10.Application.Repositories;
using MediatR;

namespace lab10.Application.UsesCases.UserRole.Command;

public record CreateUserRoleUseCaseCommand : IRequest<UserRoleKey>
{
    public Guid UserId { get; set; }

    public Guid RoleId { get; set; }
    
};

internal sealed class CreateUserRoleUseCaseCmdHandler(IUserRoleRepository repository, IMapper mapper)
    : IRequestHandler<CreateUserRoleUseCaseCommand, UserRoleKey>
{
    public async Task<UserRoleKey> Handle(CreateUserRoleUseCaseCommand request, CancellationToken cancellationToken)
    {
        var entity = mapper.Map<lab10.Domain.Entities.UserRole>(request);
        await repository.Add(entity);
        return new UserRoleKey(entity.UserId, entity.RoleId);
    }
}
