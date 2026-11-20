using AutoMapper;
using lab10.Application.DTOs;
using lab10.Application.Repositories;
using MediatR;

namespace lab10.Application.UsesCases.Role.Command;

public record UpdateRoleUseCaseCommand : IRequest<Guid>
{
    public Guid RoleId { get; set; }

    public string RoleName { get; set; } = null!;
};

internal sealed class UpdateRoleUseCaseCmdHandler(IRoleRepository repository, IMapper mapper)
    : IRequestHandler<UpdateRoleUseCaseCommand, Guid>
{
    public async Task<Guid> Handle(UpdateRoleUseCaseCommand request, CancellationToken cancellationToken)
    {
        var entity = await repository.GetById(request.RoleId)
            ?? throw new KeyNotFoundException("Role not found.");
        mapper.Map(request, entity);
        await repository.Update(entity);
        return entity.RoleId;
    }
}
