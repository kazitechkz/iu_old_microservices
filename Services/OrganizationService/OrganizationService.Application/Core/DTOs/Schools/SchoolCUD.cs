namespace OrganizationService.Application.Core.DTOs.Schools;

public class SchoolCUD
{
    public string TitleKk { get; set; }
    public string TitleRu { get; set; }
    public string TitleEn { get; set; }
    public string Code { get; set; }
    public int Status { get; set; }
    public long AreaId { get; set; }
    public long LegalFormId { get; set; }
}