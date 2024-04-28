
namespace InternetVize.Dtos
{
    public class RentalRegisterDto: UserRegisterDto
    {
        public string TaxNumber { get; set; }
        public string CompanyName { get; set; }
        public string LogoUrl { get; set; }
    }
}
