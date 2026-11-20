namespace lab10.Application.DTOs.Reports;

public record UserReportRow(string Usuario, string? Correo,
    int TotalTickets, int TicketsAbiertos, int TicketsCerrados);
