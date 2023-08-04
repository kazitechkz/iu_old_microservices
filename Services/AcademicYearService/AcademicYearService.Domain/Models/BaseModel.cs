using System.ComponentModel.DataAnnotations;

namespace AcademicYearService.Domain.Models;

public class BaseModel
{
    [Key]
    public long Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public DateTime? DeletedAt { get; set; }
    public bool IsDeleted { get; set; }
}