namespace InternetVize.Dtos
{
    public class UpdateRentalProfileDto: UpdateUserDto
    {
        public string? TaxNumber { get; set; }
        public string? CompanyName { get; set; }
        public string? LogoUrl { get; set; }
    }
}
