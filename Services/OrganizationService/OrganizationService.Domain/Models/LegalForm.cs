namespace OrganizationService.Domain.Models;

public class LegalForm : BaseModel
{
    public string TitleKk { get; set; }
    public string TitleRu { get; set; }
    public string TitleEn { get; set; }
    public string Code { get; set; }
}