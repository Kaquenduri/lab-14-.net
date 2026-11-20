using ClosedXML.Excel;
using lab10.Application.UsesCases.Ticket.Command;
using Microsoft.AspNetCore.Mvc;

namespace lab10.Api.Controllers;
[ApiController]
[Route("api/examples")]
public class ExampleController : ControllerBase
{
    [HttpPost]
    public void FirstExample()
    {
        using (var workbook = new XLWorkbook())
        {
            var worksheet = workbook.Worksheets.Add("Hoja1");
            
            worksheet.Cell(1, 1).Value = "Nombre";
            worksheet.Cell(1, 1).Value = "Edad";
            worksheet.Cell(2, 1).Value = "Juan";
            worksheet.Cell(2, 2).Value = "28";
            
            workbook.SaveAs("C:\\Users\\Administrador\\Desktop\\CARPETAS PROGRAMACION\\JRIDER\\examples\\Excel\\archivo.xlsx");
        }
    }
    
    [HttpPost("second-example")]
    public void SecondExample()
    {
        using (var workbook = 
               new XLWorkbook("C:\\Users\\Administrador\\Desktop\\CARPETAS PROGRAMACION\\JRIDER\\examples\\Excel\\archivo.xlsx"))
        {
            var worksheet = workbook.Worksheet(1);

            worksheet.Cell(2, 2).Value = 30; 
            
            workbook.Save();
        }
    }
    
    [HttpPost("third-example")]
    public void ThirdExample()
    {
        using (var workbook = new XLWorkbook())
        {
            var worksheet = workbook.Worksheets.Add(sheetName: "Datos");

            // Agregar los datos de la tabla
            worksheet.Cell(row: 1, column: 1).Value = "ID";
            worksheet.Cell(row: 1, column: 2).Value = "Nombre";
            worksheet.Cell(row: 1, column: 3).Value = "Edad";

            worksheet.Cell(row: 2, column: 1).Value = 1;
            worksheet.Cell(row: 2, column: 2).Value = "Juan";
            worksheet.Cell(row: 2, column: 3).Value = 28;

            worksheet.Cell(row: 3, column: 1).Value = 2;
            worksheet.Cell(row: 3, column: 2).Value = "María";
            worksheet.Cell(row: 3, column: 3).Value = 34;

            // Crear una tabla con los datos
            var range = worksheet.Range("A1:C3");
            range.CreateTable();

            // Guardar el archivo con la tabla
            workbook.SaveAs(file: "C:\\Users\\Administrador\\Desktop\\CARPETAS PROGRAMACION\\JRIDER\\examples\\Excel\\tabla.xlsx");
        }
    }
    
    [HttpPost("fourth-example")]
    public void FouthExample()
    {
        using (var workbook = new XLWorkbook())
        {
            var worksheet = workbook.Worksheets.Add(sheetName: "Estilos");

            // Aplicar un formato a la primera fila
            var headerRow = worksheet.Row(1);
            headerRow.Style.Font.Bold = true;
            headerRow.Style.Fill.BackgroundColor = XLColor.Cyan;
            headerRow.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

            // Agregar datos
            worksheet.Cell(row: 1, column: 1).Value = "ID";
            worksheet.Cell(row: 1, column: 2).Value = "Nombre";
            worksheet.Cell(row: 1, column: 3).Value = "Edad";

            worksheet.Cell(row: 2, column: 1).Value = 1;
            worksheet.Cell(row: 2, column: 2).Value = "Juan";
            worksheet.Cell(row: 2, column: 3).Value = 28;

            // Guardar el archivo con formato
            workbook.SaveAs(file:
                "C:\\Users\\Administrador\\Desktop\\CARPETAS PROGRAMACION\\JRIDER\\examples\\Excel\\tabla_estilos.xlsx");
        }
    }
}

