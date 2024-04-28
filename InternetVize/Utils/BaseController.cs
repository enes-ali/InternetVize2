using AutoMapper;
using InternetVize.Controllers;
using InternetVize.Dtos;
using InternetVize.Models;
using Microsoft.AspNetCore.Mvc;

namespace InternetVize.Utils
{
    public class BaseController : Controller
    {
        protected readonly ILogger<UserController> _logger;
        protected readonly IMapper _mapper;
        protected readonly AppDbContext _appDbContext;
        protected ResponseDto response = new ResponseDto();

        public BaseController(ILogger<UserController> logger, IMapper mapper, AppDbContext appDbContext)
        {
            _logger = logger;
            _mapper = mapper;
            _appDbContext = appDbContext;
        }
    }
}
