using System.ComponentModel;

namespace AcademicYearService.Domain.Models;

public class AcademicYear : BaseModel
{
    public string TitleKk { get; set; }
    public string TitleRu { get; set; }
    public string TitleEn { get; set; }
    public string Code { get; set; }
    [DefaultValue(1)]
    public int Status { get; set; }
    public DateTime StartAt { get; set; }
    public DateTime EndAt { get; set; }
}