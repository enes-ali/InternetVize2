using AutoMapper;
using InternetVize.Dtos;
using InternetVize.Models;
using Microsoft.AspNetCore.Mvc;
using InternetVize.Dtos.Vehicle;

namespace InternetVize.Controllers
{
    public class VehicleController : Controller
    {
        private readonly ILogger<UserController> _logger;
        private readonly IMapper _mapper;
        private AppDbContext _appDbContext;
        ResponseDto response = new ResponseDto();

        public VehicleController(ILogger<UserController> logger, IMapper mapper, AppDbContext appDbContext)
        {
            _logger = logger;
            _mapper = mapper;
            _appDbContext = appDbContext;
        }

        [HttpPost]
        public ResponseDto CreateVehicle(CreateVehicleDto vehicleDto)
        {
            var newVehicle = _mapper.Map<Vehicle>(vehicleDto);
            newVehicle.CreatedAt = DateTime.UtcNow;
            _appDbContext.Add(newVehicle);
            _appDbContext.SaveChanges();

            response.Succeded = true;
            response.Body = "Vehicle created successfully";
            return response;
        }

        [HttpPost]
        public ResponseDto DeleteVehicle(int id)
        {
            var vehicle = new Vehicle() { Id = id };
            _appDbContext.Vehicles.Attach(vehicle);
            _appDbContext.Vehicles.Remove(vehicle);
            _appDbContext.SaveChanges();

            response.Succeded = true;
            response.Body = "Deleted vehicle sucessfully";
            return response;
        }
    }
}
