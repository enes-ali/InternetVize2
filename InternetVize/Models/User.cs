using Microsoft.AspNetCore.Identity;

namespace InternetVize.Models
{
    public class User : IdentityUser
    {
        public string FirstName {get; set;}
        public string LastName { get; set;}
        public DateTime DateOfBirth { get; set;}
        public DateTime CreatedAt { get; set;}
        public int? BuyerProfileId { get; set; }
        public BuyerProfile? BuyerProfile { get; set;}
        public int? RentalProfileId { get; set; }
        public RentalProfile? RentalProfile { get; set;}
    }
}
