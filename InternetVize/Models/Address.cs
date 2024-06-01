namespace InternetVize.Models
{
    public class Address
    {
        public int Id { get; set; }
        public string City { get; set; }
        public string District { get; set; }
        public string Neighboorhood { get; set; }
        public string Street { get; set; }
        public string PostalCode { get; set; }
        public string AddressLine { get; set; }
        public ICollection<Vehicle> Vehicles { get; set; }
        public int? RentalProfileId { get; set; }
        public RentalProfile? RentalProfile { get; set; }
    }
}
