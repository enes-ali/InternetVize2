using InternetVize.Models;

namespace InternetVize.Dtos.Rent
{
    public class RentDto
    {
        public int Id { get; set; }
        public int? BuyerProfileId { get; set; }
        public BuyerProfile? BuyerProfile { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public decimal TotalPrice { get; set; }
        public int? HistoryBuyerProfileId { get; set; }
        public BuyerProfile? HistoryBuyerProfile { get; set; }
        public int VehicleId { get; set; }
        public Models.Vehicle Vehicle { get; set; }
    }
}
