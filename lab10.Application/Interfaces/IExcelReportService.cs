using lab10.Application.DTOs.Reports;

namespace lab10.Application.Repositories;

public interface IExcelReportService
{
    byte[] CreateTicketReport(IReadOnlyCollection<TicketReportRow> tickets);
    byte[] CreateUserReport(IReadOnlyCollection<UserReportRow> users);
}
