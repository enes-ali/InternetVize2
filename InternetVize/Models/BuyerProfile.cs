namespace InternetVize.Models
{
    public class BuyerProfile
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }
        public string IdNumber {  get; set; }
        public int? RentId {  get; set; }
        public Rent? Rent { get; set; }
        public ICollection<Rent> RentHistory { get; set; } = new List<Rent>();
    }
}
