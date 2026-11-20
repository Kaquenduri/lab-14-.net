using lab10.Application.Repositories;
using lab10.Domain.Entities;
using lab10.Infrastructure.Persistence;

namespace lab10.Infrastructure.Repositories;

public class UserRoleRepository : GenericRepository<UserRole>, IUserRoleRepository
{
    public UserRoleRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<UserRole?> GetById(Guid userId, Guid roleId)
    {
        return await _dbContext.UserRoles.FindAsync(userId, roleId);
    }

    public async Task Delete(Guid userId, Guid roleId)
    {
        var entity = await GetById(userId, roleId);
        if (entity is null) return;

        _dbContext.UserRoles.Remove(entity);
        await _dbContext.SaveChangesAsync();
    }
}
