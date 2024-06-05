using AutoMapper;
using InternetVize.Dtos;
using InternetVize.Models;
using Microsoft.AspNetCore.Mvc;
using InternetVize.Dtos.Vehicle;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using InternetVize.Dtos.Address;
using Microsoft.EntityFrameworkCore;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace InternetVize.Controllers
{
    [Route("api/Vehicle")]
    [ApiController]
    public class VehicleController : Controller
    {
        private readonly ILogger<UserController> _logger;
        private readonly IMapper _mapper;
        private AppDbContext _appDbContext;
        private readonly UserManager<User> _userManager;
        ResponseDto response = new ResponseDto();

        public VehicleController(
            ILogger<UserController> logger,
            IMapper mapper,
            AppDbContext appDbContext,
            UserManager<User> userManager)
        {
            _logger = logger;
            _mapper = mapper;
            _appDbContext = appDbContext;
            _userManager = userManager;
        }

        [HttpGet]
        public List<VehicleDto> All(
            string? userId,
            [FromQuery] string[]? brand,
            [FromQuery] string[]? model,
            GEARBOX? gearType,
            FUEL? fuelType,
            string? city, 
            string? district, 
            string? neighboorhood
        ){
            IQueryable<Vehicle> query = _appDbContext.Vehicles;

            if (brand != null && brand.Length > 0) { 
                query = query.Where(vc => brand.Contains(vc.Brand));
            }

            if (model != null && model.Length > 0)
            {
                query = query.Where(vc => model.Contains(vc.Model));
            }

            if (gearType != null)
            {
                query = query.Where(vc => gearType == vc.GearboxType);
            }

            if (fuelType != null)
            {
                query = query.Where(vc => fuelType == vc.FuelType);
            }

            // Address
            if (city != null)
            {
                query = query.Where(vc => vc.Address.City.ToLower().Equals(city));
            }
            if (district != null)
            {
                query = query.Where(vc => vc.Address.District.ToLower().Equals(district.ToLower()));
            }
            if (neighboorhood != null)
            {
                query = query.Where(vc => vc.Address.Neighboorhood.ToLower().Equals(neighboorhood.ToLower()));
            }

            if (userId != null)
            {
                var rentalProfile = _appDbContext.RentalProfiles
                    .Where(profile => profile.UserId == userId)
                    .FirstOrDefault();
                if (rentalProfile == null)
                    throw new Exception("Couldn't find a profile with the provided user id");

                query = query.Where(vc => vc.RentalProfileId == rentalProfile.Id);
            }

            var vehicleDtos = _mapper.Map<List<VehicleDto>>(query.ToList());

            foreach(var vehicleDto in vehicleDtos)
            {
                var rentalP = _appDbContext.RentalProfiles
                    .Where(rp => rp.Id == vehicleDto.RentalProfileId)
                    .Select(rp => new RentalProfile() { CompanyName = rp.CompanyName, LogoUrl = rp.LogoUrl})
                    .FirstOrDefault();
                vehicleDto.RentalProfile = rentalP;

                var address = _appDbContext.Addresses
                    .Where(addr => addr.Id == vehicleDto.AddressId)
                    .Select(addr => new Address() { City = addr.City, District = addr.District, Neighboorhood = addr.Neighboorhood })
                    .FirstOrDefault();
                vehicleDto.Address = address;
            }

            return vehicleDtos;
        }

        [HttpGet]
        [Route("{id}")]
        public VehicleDto ById(int id)
        {
            var vehicle = _appDbContext.Vehicles.Where(vc => vc.Id == id).FirstOrDefault();
            var vehicleDto = _mapper.Map<VehicleDto>(vehicle);

            var rentalP = _appDbContext.RentalProfiles
                .Where(rp => rp.Id == vehicleDto.RentalProfileId)
                .Select(rp => new RentalProfile() { CompanyName = rp.CompanyName, LogoUrl = rp.LogoUrl })
                .FirstOrDefault();
            vehicleDto.RentalProfile = rentalP;

            var address = _appDbContext.Addresses
                .Where(addr => addr.Id == vehicleDto.AddressId)
                .Select(addr => new Address() { City = addr.City, District = addr.District, Neighboorhood = addr.Neighboorhood })
                .FirstOrDefault();
            vehicleDto.Address = address;
            

            return vehicleDto;
        }

        [HttpPost]
        [Authorize(Roles = "rental")]
        public ResponseDto CreateVehicle(CreateVehicleDto vehicleDto)
        {
            var rentalProfile = _appDbContext.RentalProfiles
                .Where(profile => profile.UserId == vehicleDto.UserId)
                .FirstOrDefault();
            if (rentalProfile == null)
                throw new Exception("Couldn't find a profile with the provided user id");

            vehicleDto.RentalProfileId = rentalProfile.Id;

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

            if (updateDto.Brand != null)
            {
                vehicle.Brand = (string)updateDto.Brand;
            }

            if (updateDto.Model != null)
            {
                vehicle.Model = (string)updateDto.Model;
            }

            if (updateDto.GearboxType != null)
            {
                vehicle.GearboxType = (GEARBOX)updateDto.GearboxType;
            }

            if (updateDto.FuelType != null)
            {
                vehicle.FuelType = (FUEL)updateDto.FuelType;
            }

            if (updateDto.DailyRate != null)
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
