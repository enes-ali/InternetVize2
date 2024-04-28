using InternetVize.Models;

namespace InternetVize.Dtos.Address
{
    public class AddressDto
    {
        public int Id { get; set; }
        public string City { get; set; }
        public string District { get; set; }
        public string Neighboorhood { get; set; }
        public string Street { get; set; }
        public string PostalCode { get; set; }
        public string AddressLine { get; set; }
        public ICollection<Models.Vehicle> Vehicles { get; set; }
    }
}
