using AutoMapper;
using lab10.Application.DTOs;
using lab10.Application.Repositories;
using MediatR;

namespace lab10.Application.UsesCases.Role.Command;

public record CreateRoleUseCaseCommand : IRequest<Guid>
{
    public string RoleName { get; set; } = null!;
};

internal sealed class CreateRoleUseCaseCmdHandler(IRoleRepository repository, IMapper mapper)
    : IRequestHandler<CreateRoleUseCaseCommand, Guid>
{
    public async Task<Guid> Handle(CreateRoleUseCaseCommand request, CancellationToken cancellationToken)
    {
        var entity = mapper.Map<lab10.Domain.Entities.Role>(request);
        entity.RoleId = Guid.NewGuid();
        await repository.Add(entity);
        return entity.RoleId;
    }
}
