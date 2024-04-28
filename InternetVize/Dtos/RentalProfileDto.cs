using InternetVize.Models;

namespace InternetVize.Dtos
{
    public class RentalProfileDto
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }
        public string TaxNumber { get; set; }
        public string CompanyName { get; set; }
        public string LogoUrl { get; set; }
        public List<Models.Vehicle> Vehicles { get; set; } = new List<Models.Vehicle>();
    }
}
