
using lab10.Domain.Entities;

namespace lab10.Application.Repositories;

public interface IUserRoleRepository : IGenericRepository<UserRole>
{
    Task<UserRole?> GetById(Guid userId, Guid roleId);
    Task Delete(Guid userId, Guid roleId);
}
