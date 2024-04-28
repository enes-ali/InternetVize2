namespace InternetVize.Models
{
    public class Rent
    {
        public int Id { get; set; }
        public int? BuyerProfileId { get; set; }
        public BuyerProfile? BuyerProfile { get; set; }
        public DateTime StartTime {  get; set; }
        public DateTime EndTime { get; set; }
        public decimal TotalPrice {  get; set; }
        public int? HistoryBuyerProfileId { get; set; }
        public BuyerProfile? HistoryBuyerProfile { get; set; }
    }
}
