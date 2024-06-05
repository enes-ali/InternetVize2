namespace InternetVize.Dtos.Rent
{
    public class CreateRentDto
    {
        public int? VehicleId { get; set; }
        public string? UserId { get; set; }

        public int? BuyerProfileId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string StartTimeISO { get; set; }
        public string EndTimeISO { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
