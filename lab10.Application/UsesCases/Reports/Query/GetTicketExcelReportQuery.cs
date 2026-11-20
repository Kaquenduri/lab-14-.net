using lab10.Application.Repositories;
using lab10.Application.DTOs.Reports;
using MediatR;

namespace lab10.Application.UsesCases.Reports.Query;

public record GetTicketExcelReportQuery : IRequest<byte[]>;

internal sealed class GetTicketExcelReportQueryHandler(ITIcketRepository repository, IExcelReportService reports)
    : IRequestHandler<GetTicketExcelReportQuery, byte[]>
{
    public async Task<byte[]> Handle(GetTicketExcelReportQuery request, CancellationToken cancellationToken)
    {
        // Pedimos los datos y el servicio arma el Excel.
        var tickets = await repository.GetReportRows(cancellationToken);
        return reports.CreateTicketReport(tickets);
    }
}
