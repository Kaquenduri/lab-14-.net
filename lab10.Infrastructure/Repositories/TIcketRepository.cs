using Microsoft.EntityFrameworkCore;
using lab10.Application.Repositories;
using lab10.Domain.Entities;
using lab10.Infrastructure.Persistence;

namespace lab10.Infrastructure.Repositories;

public class TIcketRepository : GenericRepository<Ticket>, ITIcketRepository
{
    public TIcketRepository(AppDbContext context) : base(context)
    {
        
    }

    // Traemos solo los datos que van a aparecer en el Excel.
    public async Task<IReadOnlyCollection<lab10.Application.DTOs.Reports.TicketReportRow>> GetReportRows(CancellationToken cancellationToken)
    {
        return await _dbContext.Tickets.AsNoTracking()
            .OrderBy(t => t.CreatedAt)
            .Select(t => new lab10.Application.DTOs.Reports.TicketReportRow(
                t.Title, t.Status, t.User.Username, t.Responses.Count))
            .ToListAsync(cancellationToken);
    }
}

