using ClosedXML.Excel;
using JambOfficeApp.Models;

namespace JambOfficeApp.Services;

public class ExportService
{
    public void ExportCandidatesToExcel(List<Candidate> candidates, string filePath)
    {
        using var workbook = new XLWorkbook();
        var sheet = workbook.Worksheets.Add("Candidates");

        
        // Header rows
        sheet.Cell(1, 1).Value = "Reg Number";
        sheet.Cell(1, 2).Value = "Full Name";
        sheet.Cell(1, 3).Value = "Date of Birth";
        sheet.Cell(1, 4).Value = "Gender";
        sheet.Cell(1, 5).Value = "Phone Number";
        sheet.Cell(1, 6).Value = "Email";
        sheet.Cell(1, 7).Value = "Institution Applied";
        sheet.Cell(1, 8).Value = "Department Applied";
        sheet.Cell(1, 9).Value = "JAMB Score";
        
        var headerRow = sheet.Row(1);
        headerRow.Style.Font.Bold = true;
        
        // Data rows
        int row = 2;
        foreach (var c in candidates)
        {
            sheet.Cell(row, 1).Value =  c.RegNumber;
            sheet.Cell(row, 2).Value =  c.FullName;
            sheet.Cell(row, 3).Value =  c.DateOfBirth;
            sheet.Cell(row, 4).Value =  c.Gender;
            sheet.Cell(row, 5).Value =  c.PhoneNumber;
            sheet.Cell(row, 6).Value =  c.Email;
            sheet.Cell(row, 7).Value =  c.InstitutionApplied;
            sheet.Cell(row, 8).Value =  c.DepartmentApplied;
            sheet.Cell(row, 9).Value =  c.JambScore;
            row++;
        }

        sheet.Columns().AdjustToContents();
        
        workbook.SaveAs(filePath);
    }
}