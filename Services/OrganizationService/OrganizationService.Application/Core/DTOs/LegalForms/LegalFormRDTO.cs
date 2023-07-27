using Application.Core.DTOs;

namespace OrganizationService.Application.Core.DTOs.LegalForms;

public class LegalFormRDTO : BaseDTO
{
    public string TitleKk { get; set; }
    public string TitleRu { get; set; }
    public string TitleEn { get; set; }
    public string Code { get; set; }
}