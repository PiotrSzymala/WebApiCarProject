using System.ComponentModel.DataAnnotations;

namespace WebApiCarProject.Infrastructure.Entities
{
    public class BaseEntity
    {
        [Key]
        public long Id { get; init; }
        public DateTime CreatedAtUtc { get; init; } = DateTime.UtcNow;
    }
}
