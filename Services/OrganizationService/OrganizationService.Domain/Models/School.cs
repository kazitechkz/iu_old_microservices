namespace OrganizationService.Domain.Models;

public class School : BaseModel
{
    public string TitleKk { get; set; }
    public string TitleRu { get; set; }
    public string TitleEn { get; set; }
    public string Code { get; set; }
    public int Status { get; set; } = 1;
    
    public long AreaId { get; set; }
    public Area Area { get; set; }
    public long LegalFormId { get; set; }
    public LegalForm LegalForm { get; set; }
}