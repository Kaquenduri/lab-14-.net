using Microsoft.EntityFrameworkCore;
using lab10.Application.Repositories;
using lab10.Domain.Entities;
using lab10.Infrastructure.Persistence;

namespace lab10.Infrastructure.Repositories;

public class UserRepository : GenericRepository<User>, IUserRepository
{
    public UserRepository(AppDbContext context) : base(context)
    {
        
    }

    // Resumimos los tickets de cada usuario desde la base de datos.
    public async Task<IReadOnlyCollection<lab10.Application.DTOs.Reports.UserReportRow>> GetReportRows(CancellationToken cancellationToken)
    {
        return await _dbContext.Users.AsNoTracking()
            .OrderBy(u => u.Username)
            .Select(u => new lab10.Application.DTOs.Reports.UserReportRow(
                u.Username, u.Email, u.Tickets.Count,
                u.Tickets.Count(t => t.ClosedAt == null),
                u.Tickets.Count(t => t.ClosedAt != null)))
            .ToListAsync(cancellationToken);
    }
}

