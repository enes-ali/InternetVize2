namespace InternetVize.Dtos.Address
{
    public class CreateAddressDto
    {
        public string UserId {  get; set; }
        public int? RentalProfileId { get; set; }
        public string City { get; set; }
        public string District { get; set; }
        public string Neighboorhood { get; set; }
        public string Street { get; set; }
        public string PostalCode { get; set; }
        public string AddressLine { get; set; }
    }
}
