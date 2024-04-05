using System.ComponentModel.DataAnnotations;

namespace WebApiCar.Infrastructure.Entities
{
    public class BaseEntity
    {
        [Key]
        public long Id { get; init; }
        public DateTime CreatedAtUtc { get; init; } = DateTime.UtcNow;
    }
}
