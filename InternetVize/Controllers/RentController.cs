using AutoMapper;
using InternetVize.Dtos.Rent;
using InternetVize.Dtos;
using InternetVize.Models;
using Microsoft.AspNetCore.Mvc;
using InternetVize.Utils;

namespace InternetVize.Controllers
{
    public class RentController: BaseController
    {

        public RentController(ILogger<UserController> logger, IMapper mapper, AppDbContext appDbContext): base(logger, mapper, appDbContext) { }

        [HttpPost]
        public ResponseDto CreateRent(CreateRentDto rentDto)
        {
            var newRent = _mapper.Map<Rent>(rentDto);
            _appDbContext.Add(newRent);
            _appDbContext.SaveChanges();

            response.Succeded = true;
            response.Body = "Rent created successfully";
            return response;
        }
    }
}
