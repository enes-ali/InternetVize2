using AutoMapper;
using InternetVize.Dtos;
using InternetVize.Dtos.Address;
using InternetVize.Dtos.Rent;
using InternetVize.Dtos.Vehicle;
using InternetVize.Models;

namespace InternetVize.Mapping
{
    public class MapProfile: Profile
    {
        public MapProfile() {
            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<User, UserRegisterDto>().ReverseMap();


            CreateMap<BuyerProfile, BuyerProfileDto>().ReverseMap();
            CreateMap<BuyerProfile, BuyerRegisterDto>().ReverseMap();
            
            CreateMap<RentalProfile, RentalProfileDto>().ReverseMap();
            CreateMap<RentalProfile, RentalRegisterDto>().ReverseMap();


            CreateMap<Vehicle, VehicleDto>().ReverseMap();
            CreateMap<Vehicle, CreateVehicleDto>().ReverseMap();

            CreateMap<Rent, RentDto>().ReverseMap();
            CreateMap<Rent, CreateRentDto>().ReverseMap();

            CreateMap<Address, AddressDto>().ReverseMap();
            CreateMap<Address, CreateAddressDto>().ReverseMap();
        }
    }
}
