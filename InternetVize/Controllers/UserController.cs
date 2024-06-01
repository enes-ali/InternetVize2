using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using InternetVize.Models;
using InternetVize.Dtos;
using AutoMapper;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace InternetVize.Controllers
{
    [Route("api/User/[action]")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;
        private readonly ILogger<UserController> _logger;
        private readonly IMapper _mapper;
        private AppDbContext _appDbContext;
        private readonly IConfiguration _configuration;
        ResponseDto response = new ResponseDto();

        public UserController(UserManager<User> userManager, RoleManager<Role> roleManager, ILogger<UserController> logger, IMapper mapper, AppDbContext appDbContext, IConfiguration config)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _logger = logger;
            _mapper = mapper;
            _appDbContext = appDbContext;
            _configuration = config;
        }

        [HttpGet]
        public List<RentalProfileDto> RentalProfiles(string? companyName) {
            IQueryable<RentalProfile> query = _appDbContext.RentalProfiles;

            if(companyName != null)
            {
                query = query.Where(rp => rp.CompanyName == companyName);
            }
            
            var profiles = query.ToList();
            var profileDtos = _mapper.Map<List<RentalProfileDto>>(profiles);
            return profileDtos;
        }

        [HttpGet]
        [Route("{id}")]
        public UserDto ByUserId(string id)
        {
            var user = _userManager.Users.Where(user => user.Id == id).SingleOrDefault();
            
            return _mapper.Map<UserDto>(user);
        }

        [HttpPost]
        public async Task<ResponseDto> RegisterRentalProfile(RentalRegisterDto registerDto)
        {
            try
            {
                var newUserId = await CreateIdentityUser(registerDto, "rental");

                if (_appDbContext.RentalProfiles.Count(profile => profile.CompanyName == registerDto.CompanyName) > 0)
                {
                    response.Body = "There is already a Rental Company with the provided Company Name";
                    response.Succeded = false;
                    return response;
                }

                var newRentalProfile = _mapper.Map<RentalProfile>(registerDto);
                newRentalProfile.UserId = newUserId;
                _appDbContext.Add(newRentalProfile);
                _appDbContext.SaveChanges();

                response.Body = "You have been registered successfully";
                response.Succeded = true;
                return response;
            }
            catch (Exception ex)
            {
                response.Body = ex.Message;
                response.Succeded = false;
                return response;
            }
        }


        [HttpPut]
        public ResponseDto UpdateBuyerProfile(UpdateBuyerProfileDto updateDto)
        {
            var profile = _appDbContext.BuyerProfiles.Where(profile => profile.Id == updateDto.Id).FirstOrDefault();
            if (profile == null)
            {
                response.Succeded = false;
                response.Body = "Couldnt find a buyer profile with the provided Id";
                return response;
            }

            var user = _userManager.Users.Where(usr => usr.Id == profile.UserId).FirstOrDefault();

            if (updateDto.FirstName != null)
            {
                user.FirstName = (string)updateDto.FirstName;
            }

            if (updateDto.LastName != null)
            {
                user.LastName = (string)updateDto.LastName;
            }

            if (updateDto.Email != null)
            {
                user.Email = (string)updateDto.Email;
            }

            if (updateDto.PhoneNumber != null)
            {
                user.PhoneNumber = (string)updateDto.PhoneNumber;
            }

            if (updateDto.DateOfBirth != null)
            {
                user.DateOfBirth = (DateTime)updateDto.DateOfBirth;
            }

            if (updateDto.IdNumber != null)
            {
                profile.IdNumber = (string)updateDto.IdNumber;
            }

            _appDbContext.SaveChanges();

            response.Succeded = true;
            response.Body = "Profile updated successfully";
            return response;
        }

        [HttpPost]
        public async Task<ResponseDto> RegisterBuyerProfile(BuyerRegisterDto registerDto)
        {
            try
            {
                var newUserId = await CreateIdentityUser(registerDto, "buyer");

                if (_appDbContext.BuyerProfiles.Count(profile => profile.IdNumber == registerDto.IdNumber) > 0)
                {
                    response.Body = "There is already an account with the provided ID Number";
                    response.Succeded = false;
                    return response;
                }

                var newBuyerProfile = _mapper.Map<BuyerProfile>(registerDto);
                newBuyerProfile.UserId = newUserId;
                _appDbContext.Add(newBuyerProfile);
                _appDbContext.SaveChanges();

                response.Body = "You have been registered successfully";
                response.Succeded = true;
                return response;
            }
            catch (Exception ex)
            {
                response.Body = ex.Message;
                response.Succeded = false;
                return response;
            }
        }

        [HttpPost]
        public async Task<ResponseDto> SignIn(LoginDto loginDto)
        {
            var user = await _userManager.FindByEmailAsync(loginDto.Email);

            if (user == null) {
                response.Body = "Couldn't find a user with the provided email";
                response.Succeded = false;
                return response;
            }

            if(!await _userManager.CheckPasswordAsync(user, loginDto.Password))
            {
                response.Body = "Incorrect Password";
                response.Succeded = false;
                return response;
            }

            var roles = await _userManager.GetRolesAsync(user);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, user.Email!),
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim("JWTID", Guid.NewGuid().ToString()),
            };

            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var token = GenerateJWT(claims);

            response.Succeded = true;
            response.Body = token;
            return response;
        }

        private async Task<string> CreateIdentityUser(UserRegisterDto userDto, string role)
        {
            var identityResult = await _userManager.CreateAsync(new() { UserName = userDto.UserName, Email = userDto.Email, FirstName = userDto.FirstName, LastName = userDto.LastName, PhoneNumber = userDto.PhoneNumber }, userDto.Password);

            if (!identityResult.Succeeded)
            {
                string errorHtml = "";
                foreach (var item in identityResult.Errors)
                {
                    errorHtml += $"<p>{item.Description}</p>";
                }

                throw new Exception(errorHtml);
            }

            var user = await _userManager.FindByNameAsync(userDto.UserName);
            var roleExist = await _roleManager.RoleExistsAsync(role);
            if (!roleExist)
            {
                var newRole = new Role { Name = role };
                await _roleManager.CreateAsync(newRole);
            }

            await _userManager.AddToRoleAsync(user!, role);

            return user!.Id;
        }

        private string GenerateJWT(List<Claim> claims)
        {

            var accessTokenExpiration = DateTime.Now.AddMinutes(Convert.ToDouble(_configuration["AccessTokenExpiration"]));


            var authSecret = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));

            var tokenObject = new JwtSecurityToken(
                    issuer: _configuration["Jwt:Issuer"],
                    audience: _configuration["Jwt:Audience"],
                    expires: accessTokenExpiration,
                    claims: claims,
                    signingCredentials: new SigningCredentials(authSecret, SecurityAlgorithms.HmacSha256)
                );

            string token = new JwtSecurityTokenHandler().WriteToken(tokenObject);

            return token;
        }
    }
}
