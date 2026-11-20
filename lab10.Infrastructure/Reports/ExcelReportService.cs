using ClosedXML.Excel;
using lab10.Application.DTOs.Reports;
using lab10.Application.Repositories;

namespace lab10.Infrastructure.Reports;

public class ExcelReportService : IExcelReportService
{
    public byte[] CreateTicketReport(IReadOnlyCollection<TicketReportRow> tickets)
    {
        using var book = new XLWorkbook();
        var sheet = book.Worksheets.Add("Tickets");
        SetHeaders(sheet, "Título", "Estado", "Usuario", "Respuestas");

        var row = 2;
        foreach (var ticket in tickets)
        {
            sheet.Cell(row, 1).Value = ticket.Titulo;
            sheet.Cell(row, 2).Value = ticket.Estado;
            sheet.Cell(row, 3).Value = ticket.Usuario;
            sheet.Cell(row, 4).Value = ticket.Respuestas;
            row++;
        }

        return Save(book, sheet, 4);
    }

    public byte[] CreateUserReport(IReadOnlyCollection<UserReportRow> users)
    {
        using var book = new XLWorkbook();
        var sheet = book.Worksheets.Add("Usuarios");
        SetHeaders(sheet, "Usuario", "Correo", "Total tickets", "Sin cierre", "Cerrados");

        var row = 2;
        foreach (var user in users)
        {
            sheet.Cell(row, 1).Value = user.Usuario;
            sheet.Cell(row, 2).Value = user.Correo ?? "";
            sheet.Cell(row, 3).Value = user.TotalTickets;
            sheet.Cell(row, 4).Value = user.TicketsAbiertos;
            sheet.Cell(row, 5).Value = user.TicketsCerrados;
            row++;
        }

        return Save(book, sheet, 5);
    }

    private static void SetHeaders(IXLWorksheet sheet, params string[] titles)
    {
        for (var column = 0; column < titles.Length; column++)
            sheet.Cell(1, column + 1).Value = titles[column];
        sheet.Range(1, 1, 1, titles.Length).Style.Font.Bold = true;
    }

    private static byte[] Save(XLWorkbook book, IXLWorksheet sheet, int columns)
    {
        // Ajustamos las columnas para que se pueda leer el reporte.
        sheet.Columns(1, columns).AdjustToContents();
        using var stream = new MemoryStream();
        book.SaveAs(stream);
        return stream.ToArray();
    }
}
