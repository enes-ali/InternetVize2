using AutoMapper;
using InternetVize.Dtos;
using InternetVize.Dtos.Address;
using InternetVize.Models;
using Microsoft.AspNetCore.Identity;
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
        private readonly UserManager<User> _userManager;
        ResponseDto response = new ResponseDto();

        public AddressController(
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
        public List<AddressDto> GetAddresses(string userId) {
            var rentalProfile = _appDbContext.RentalProfiles
                 .Where(profile => profile.UserId == userId)
                 .FirstOrDefault();
            if (rentalProfile == null)
                throw new Exception("Couldn't find a profile with the provided user id");

            var addresses = _appDbContext.Addresses
                .Where(address => address.RentalProfileId == rentalProfile.Id)
                .ToList();

            return _mapper.Map<List<AddressDto>>(addresses);
        }

        [HttpPost]
        public ResponseDto CreateAddress(CreateAddressDto addressDto)
        {
            var rentalProfile = _appDbContext.RentalProfiles
                .Where(profile => profile.UserId == addressDto.UserId)
                .FirstOrDefault();
            if (rentalProfile == null) 
                throw new Exception("Couldn't find a profile with the provided user id");

            addressDto.RentalProfileId = rentalProfile.Id;
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
