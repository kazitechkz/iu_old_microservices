namespace AcademicYearService.Application.Core.DTOs.AcademicYears;

public class AcademicYearRDTO : BaseDTO
{
    public string TitleKk { get; set; }
    public string TitleRu { get; set; }
    public string TitleEn { get; set; }
    public string Code { get; set; }
    public int Status { get; set; }
    public DateTime StartAt { get; set; }
    public DateTime EndAt { get; set; }
}