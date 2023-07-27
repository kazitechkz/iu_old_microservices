using System.ComponentModel.DataAnnotations;

namespace AcademicYearService.Domain.Models;

public class BaseModel
{
    [Key]
    public long Id { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime? UpdatedAt { get; set; } = DateTime.Now;
    public DateTime? DeletedAt { get; set; } = DateTime.Now;
    public bool IsDeleted { get; set; }
}