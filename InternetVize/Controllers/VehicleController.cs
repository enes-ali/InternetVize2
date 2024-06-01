using AutoMapper;
using InternetVize.Dtos;
using InternetVize.Models;
using Microsoft.AspNetCore.Mvc;
using InternetVize.Dtos.Vehicle;
using Microsoft.AspNetCore.Authorization;

namespace InternetVize.Controllers
{
    [Route("api/Vehicle")]
    [ApiController]
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

        [HttpGet]
        public List<VehicleDto> All(int? rentalId, string? brand, string? model)
        {
            IQueryable<Vehicle> query = _appDbContext.Vehicles;
            
            if(brand != null) { 
                query = query.Where(vc => vc.Brand == brand);
            }

            if (model != null)
            {
                query = query.Where(vc => vc.Model == model);
            }

            if (rentalId != null)
            {
                query = query.Where(vc => vc.RentalProfileId == rentalId);
            }

            var vehicleDtos = _mapper.Map<List<VehicleDto>>(query.ToList());

            return vehicleDtos;
        }

        [HttpGet]
        [Route("{id}")]
        public VehicleDto ById(int id)
        {
            var vehicle = _appDbContext.Vehicles.Where(vc => vc.Id == id).FirstOrDefault();
            var vehicleDto = _mapper.Map<VehicleDto>(vehicle);

            return vehicleDto;
        }

        [HttpPost]
        [Authorize(Roles = "rental")]
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

        [HttpPut]
        [Authorize(Roles = "rental")]
        public ResponseDto UpdateVehicle(UpdateVehicleDto updateDto)
        {
            var vehicle = _appDbContext.Vehicles.Where(vehicle => vehicle.Id == updateDto.Id).FirstOrDefault();
            
            if (vehicle == null)
            {
                response.Succeded = false;
                response.Body = "Couldnt find vehicle with the provided Id";
                return response;
            }

            if(updateDto.DailyRate != null)
            {
                vehicle.DailyRate = (decimal)updateDto.DailyRate;
            }

            if (updateDto.MinBuyerAge != null)
            {
                vehicle.MinBuyerAge = (uint)updateDto.MinBuyerAge;
            }

            if (updateDto.Deposit != null)
            {
                vehicle.Deposit = (uint)updateDto.Deposit;
            }

            if (updateDto.PictureUrl != null)
            {
                vehicle.PictureUrl = (string)updateDto.PictureUrl;
            }

            if (updateDto.AddressId != null)
            {
                vehicle.AddressId = (int)updateDto.AddressId;
            }

            _appDbContext.SaveChanges();

            response.Succeded = true;
            response.Body = "Vehicle updated successfully";
            return response;
        }

        [HttpDelete]
        [Authorize(Roles = "rental")]
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
