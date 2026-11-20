using lab10.Application.UsesCases.Reports.Query;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace lab10.Api.Controllers;

[ApiController]
[Route("api/reports")]
public class ReportsController(IMediator mediator) : ControllerBase
{
    private const string ExcelType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
    
    
    [HttpGet("tickets")]
    [Produces(ExcelType)]
    public async Task<IActionResult> Tickets(CancellationToken cancellationToken)
    {
        var excel = await mediator.Send(new GetTicketExcelReportQuery(), cancellationToken);
        return File(excel, ExcelType, "reporte-tickets.xlsx");
    }
    
    
    [HttpGet("usuarios")]
    [Produces(ExcelType)]
    public async Task<IActionResult> Users(CancellationToken cancellationToken)
    {
        var excel = await mediator.Send(new GetUserExcelReportQuery(), cancellationToken);
        return File(excel, ExcelType, "reporte-usuarios.xlsx");
    }
}
