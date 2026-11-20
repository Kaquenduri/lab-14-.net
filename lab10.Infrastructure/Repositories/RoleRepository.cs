using lab10.Application.Repositories;
using lab10.Domain.Entities;
using lab10.Infrastructure.Persistence;

namespace lab10.Infrastructure.Repositories;

public class RoleRepository : GenericRepository<Role>, IRoleRepository
{
    public RoleRepository(AppDbContext context) : base(context)
    {
        
    }
}