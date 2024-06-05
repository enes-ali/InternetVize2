using AutoMapper;
using InternetVize.Dtos.Rent;
using InternetVize.Dtos;
using InternetVize.Models;
using Microsoft.AspNetCore.Mvc;
using InternetVize.Utils;
using InternetVize.Dtos.Vehicle;
using Microsoft.EntityFrameworkCore;

namespace InternetVize.Controllers
{
    [Route("api/Rent")]
    [ApiController]
    public class RentController: BaseController
    {
        public RentController(ILogger<UserController> logger, IMapper mapper, AppDbContext appDbContext): base(logger, mapper, appDbContext) { }

        [HttpGet]
        [Route("{id}")]
        public List<RentDto> ByUserId(string id)
        {
            var buyerProfile = _appDbContext.BuyerProfiles
                .Where(profile => profile.UserId == id)
                .FirstOrDefault();
            var rentalProfile = _appDbContext.RentalProfiles
                    .Where(profile => profile.UserId == id)
                    .FirstOrDefault();
            if (buyerProfile == null && rentalProfile == null)
                throw new Exception("Couldn't find a profile with the provided user id");

            var rent = _appDbContext.Rents
                .Where(r => buyerProfile != null ? r.BuyerProfileId == buyerProfile.Id : r.Vehicle.RentalProfileId == rentalProfile.Id)
                .ToList();
            var rentDtos = _mapper.Map<List<RentDto>>(rent);

            foreach(RentDto rentDto in rentDtos) {
                var vehicle = _appDbContext.Vehicles.Where(vehicle => vehicle.Id == rentDto.VehicleId).FirstOrDefault();
                
                if (vehicle != null && buyerProfile != null)
                {
                    var vRentalProfile = _appDbContext.RentalProfiles.Where(profile => profile.Id == vehicle.RentalProfileId).FirstOrDefault();
                    vehicle.RentalProfile = vRentalProfile;
                    var address = _appDbContext.Addresses.Where(address => address.Id == vehicle.AddressId).FirstOrDefault();
                    vehicle.Address = address;
                }

                else if (rentalProfile != null)
                {
                    var vBuyerProfile = _appDbContext.BuyerProfiles
                        .Where(profile => profile.Id == rentDto.BuyerProfileId)
                        .Include(bp => bp.User)
                        .FirstOrDefault();
                    rentDto.BuyerProfile = vBuyerProfile;
                }

                rentDto.Vehicle = vehicle;
            }

            return rentDtos;
        }

        [HttpPost]
        public ResponseDto CreateRent(CreateRentDto rentDto)
        {
            if(_appDbContext.Rents.Count(rent => rent.BuyerProfileId == rentDto.BuyerProfileId) > 0)
            {
                response.Succeded = false;
                response.Body = "You cannot rent more then one vehicle";
                return response;
            }

            if(_appDbContext.Rents.Count(rent => rent.VehicleId == rentDto.VehicleId) > 0) {
                response.Succeded = false;
                response.Body = "You cannot rent a car that is already rented";
                return response;
            }

            var buyerProfile = _appDbContext.BuyerProfiles
                .Where(profile => profile.UserId == rentDto.UserId)
                .FirstOrDefault();
            if (buyerProfile == null)
                throw new Exception("Couldn't find a profile with the provided user id");

            rentDto.BuyerProfileId = buyerProfile.Id;
            rentDto.StartTime = DateTime.Parse(rentDto.StartTimeISO, null, System.Globalization.DateTimeStyles.RoundtripKind);
            rentDto.EndTime = DateTime.Parse(rentDto.EndTimeISO, null, System.Globalization.DateTimeStyles.RoundtripKind);

            var newRent = _mapper.Map<Rent>(rentDto);
            _appDbContext.Add(newRent);
            _appDbContext.SaveChanges();

            response.Succeded = true;
            response.Body = "Rent created successfully";
            return response;
        }
    }
}
