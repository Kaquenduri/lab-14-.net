using lab10.Application.DTOs.Reports;
using lab10.Domain.Entities;

namespace lab10.Application.Repositories;

public interface IUserRepository : IGenericRepository<User>
{
    Task<IReadOnlyCollection<UserReportRow>> GetReportRows(CancellationToken cancellationToken);
}
