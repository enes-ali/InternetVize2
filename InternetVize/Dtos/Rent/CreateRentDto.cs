namespace InternetVize.Dtos.Rent
{
    public class CreateRentDto
    {
        public int? VehicleId { get; set; }
        public int? BuyerProfileId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
