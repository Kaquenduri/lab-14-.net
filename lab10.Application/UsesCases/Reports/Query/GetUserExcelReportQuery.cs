using lab10.Application.Repositories;
using lab10.Application.DTOs.Reports;
using MediatR;

namespace lab10.Application.UsesCases.Reports.Query;

public record GetUserExcelReportQuery : IRequest<byte[]>;

internal sealed class GetUserExcelReportQueryHandler(IUserRepository repository, IExcelReportService reports)
    : IRequestHandler<GetUserExcelReportQuery, byte[]>
{
    public async Task<byte[]> Handle(GetUserExcelReportQuery request, CancellationToken cancellationToken)
    {
        // Pedimos los datos y el servicio arma el Excel.
        var users = await repository.GetReportRows(cancellationToken);
        return reports.CreateUserReport(users);
    }
}
