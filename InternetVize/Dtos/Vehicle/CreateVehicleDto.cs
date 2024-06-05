using InternetVize.Models;

namespace InternetVize.Dtos.Vehicle
{
    public class CreateVehicleDto
    {
        public string UserId { get; set; }
        public int RentalProfileId { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public GEARBOX GearboxType { get; set; }
        public FUEL FuelType { get; set; }
        public uint MinBuyerAge { get; set; }
        public uint Deposit { get; set; }
        public decimal DailyRate { get; set; }
        public string PictureUrl { get; set; }
        public int AddressId { get; set; }
    }
}
