namespace WebApiCar.Infrastructure.Entities
{
    public class Car : BaseEntity
    {
        public string Brand { get; set; } = default!;
        public string RegistryPlate { get; set; } = default!;
        public string VinNumber { get; set; } = default!;
        public bool IsAvailable { get; set; }
    }
}