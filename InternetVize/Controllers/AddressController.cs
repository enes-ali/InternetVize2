using AutoMapper;
using InternetVize.Dtos;
using InternetVize.Dtos.Address;
using InternetVize.Models;
using Microsoft.AspNetCore.Mvc;

namespace InternetVize.Controllers
{
    [Route("api/Address")]
    [ApiController]
    public class AddressController : Controller
    {
        private readonly ILogger<UserController> _logger;
        private readonly IMapper _mapper;
        private AppDbContext _appDbContext;
        ResponseDto response = new ResponseDto();

        public AddressController(ILogger<UserController> logger, IMapper mapper, AppDbContext appDbContext)
        {
            _logger = logger;
            _mapper = mapper;
            _appDbContext = appDbContext;
        }

        [HttpPost]
        public ResponseDto CreateAddress(CreateAddressDto addressDto)
        {
            var newAddress = _mapper.Map<Address>(addressDto);
            _appDbContext.Add(newAddress);
            _appDbContext.SaveChanges();

            response.Succeded = true;
            response.Body = "Address created successfully";
            return response;
        }

        [HttpDelete]
        public ResponseDto DeleteAddress(int id)
        {
            var address = new Address() { Id = id };
            _appDbContext.Addresses.Attach(address);
            _appDbContext.Addresses.Remove(address);
            _appDbContext.SaveChanges();

            response.Succeded = true;
            response.Body = "Deleted address sucessfully";
            return response;
        }
    }
}
