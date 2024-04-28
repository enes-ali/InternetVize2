using InternetVize.Models;

namespace InternetVize.Dtos
{
    public class BuyerProfileDto: UserDto
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }
        public string IdNumber { get; set; }
        public int? RentId { get; set; }
        public Models.Rent? Rent { get; set; }
        public List<Models.Rent> RentHistory { get; set; } = new List<Models.Rent>();
    }
}
