using System.ComponentModel.DataAnnotations;

namespace WebApiCarProject.Infrastructure.Entities;

public class BaseEntity
{
    [Key] 
    public int Id { get; init; }

    public DateTime CreatedAtUtc { get; init; } = DateTime.UtcNow;
}