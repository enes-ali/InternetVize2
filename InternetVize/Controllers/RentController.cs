using AutoMapper;
using InternetVize.Dtos.Rent;
using InternetVize.Dtos;
using InternetVize.Models;
using Microsoft.AspNetCore.Mvc;
using InternetVize.Utils;

namespace InternetVize.Controllers
{
    [Route("api/Rent")]
    [ApiController]
    public class RentController: BaseController
    {
        public RentController(ILogger<UserController> logger, IMapper mapper, AppDbContext appDbContext): base(logger, mapper, appDbContext) { }

        [HttpGet]
        [Route("{id}")]
        public RentDto ById(int id)
        {
            var rent = _appDbContext.Rents.Where(r => r.Id == id).FirstOrDefault();
            var rentDto = _mapper.Map<RentDto>(rent);

            return rentDto;
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

            var newRent = _mapper.Map<Rent>(rentDto);
            _appDbContext.Add(newRent);
            _appDbContext.SaveChanges();

            response.Succeded = true;
            response.Body = "Rent created successfully";
            return response;
        }
    }
}
