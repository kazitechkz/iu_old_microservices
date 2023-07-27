using AcademicYearService.Domain.Models;

namespace AcademicYearService.Application.Core.DTOs.Terms;

public class TermRDTO : BaseDTO
{
    public long AcademicYearId { get; set; }
    public virtual AcademicYear AcademicYear { get; set; }
    public string TitleKk { get; set; }
    public string TitleRu { get; set; }
    public string TitleEn { get; set; }
    public string Code { get; set; }
    public int Status { get; set; }
    public DateTime StartAt { get; set; }
    public DateTime EndAt { get; set; }
}