using Application.Core.DTOs;
using OrganizationService.Application.Core.DTOs.Areas;
using OrganizationService.Application.Core.DTOs.LegalForms;
using OrganizationService.Domain.Models;

namespace OrganizationService.Application.Core.DTOs.Schools;

public class SchoolRDTO : BaseDTO
{
    public string TitleKk { get; set; }
    public string TitleRu { get; set; }
    public string TitleEn { get; set; }
    public string Code { get; set; }
    public int Status { get; set; }
    public long AreaId { get; set; }
    public virtual AreaRDTO Area { get; set; }
    public long LegalFormId { get; set; }
    public virtual LegalFormRDTO LegalForm { get; set; }
}