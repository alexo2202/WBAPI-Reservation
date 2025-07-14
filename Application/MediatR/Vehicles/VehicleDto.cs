namespace Application.MediatR.Vehicles
{
    public class VehicleDto
    {
        public int Id { get; set; }
        public string PlateNumber { get; set; } = null!;
        public string Brand { get; set; } = null!;
        public string Model { get; set; } = null!;
        public int Year { get; set; }
        public int VehicleTypeId { get; set; }
        public int BookingValuePerDay { get; set; }
    }
}
