using lab10.Application.DTOs.Reports;
using lab10.Domain.Entities;
namespace lab10.Application.Repositories;

public interface ITIcketRepository : IGenericRepository<Ticket>
{
    Task<IReadOnlyCollection<TicketReportRow>> GetReportRows(CancellationToken cancellationToken);
}
