namespace InternetVize.Models
{
    public enum GEARBOX {
        AUTOMATIC,
        MANUAL
    }

    public enum FUEL
    {
        GASOLINE,
        DIESEL,
        PETROL,
        ELECTRIC
    }

    public class Vehicle
    {
        public int Id { get; set; }
        public int RentalProfileId {  get; set; }
        public RentalProfile RentalProfile { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Brand {  get; set; }
        public string Model { get; set; }
        public GEARBOX GearboxType { get; set; }
        public FUEL FuelType { get; set; }
        public uint MinBuyerAge {  get; set; }
        public uint Deposit {  get; set; }
        public decimal DailyRate {  get; set; }
        public string PictureUrl {  get; set; }
        public int? AddressId {  get; set; }
        public Address? Address { get; set; }
    }
}
