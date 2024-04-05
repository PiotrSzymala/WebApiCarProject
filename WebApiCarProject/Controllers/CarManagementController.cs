using Microsoft.AspNetCore.Mvc;
using WebApiCarProject.Infrastructure.Repositories;

namespace WebApiCarProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarManagementController
    {
        private readonly ICarRepository _carRepository;

        public CarManagementController(ICarRepository carService)
        {
            _carRepository = carService;
        }

    }
}
