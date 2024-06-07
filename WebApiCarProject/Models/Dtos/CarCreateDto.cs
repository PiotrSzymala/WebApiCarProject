namespace WebApiCarProject.Models.Dtos
{
    public class CarCreateDto
    {
        public string Brand { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
        public string RegistryPlate { get; set; }
        public string VinNumber { get; set; }
        public bool IsAvailable { get; set; }
    }
}
