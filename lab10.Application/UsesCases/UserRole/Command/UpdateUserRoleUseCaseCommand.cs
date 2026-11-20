using AutoMapper;
using lab10.Application.DTOs;
using lab10.Application.Repositories;
using MediatR;

namespace lab10.Application.UsesCases.UserRole.Command;

public record UpdateUserRoleUseCaseCommand : IRequest<UserRoleKey>
{
    public Guid UserId { get; set; }

    public Guid RoleId { get; set; }
    
};

internal sealed class UpdateUserRoleUseCaseCmdHandler(IUserRoleRepository repository, IMapper mapper)
    : IRequestHandler<UpdateUserRoleUseCaseCommand, UserRoleKey>
{
    public async Task<UserRoleKey> Handle(UpdateUserRoleUseCaseCommand request, CancellationToken cancellationToken)
    {
        var entity = await repository.GetById(request.UserId, request.RoleId)
            ?? throw new KeyNotFoundException("UserRole not found.");
        mapper.Map(request, entity);
        await repository.Update(entity);
        return new UserRoleKey(entity.UserId, entity.RoleId);
    }
}
