namespace InternetVize.Models
{
    public class RentalProfile
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }
        public string TaxNumber { get; set; }
        public string CompanyName { get; set; }
        public string LogoUrl { get; set; }
        public ICollection<Vehicle> Vehicles { get; set; } = new List<Vehicle>();
    }
}
