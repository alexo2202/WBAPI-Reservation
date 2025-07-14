using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Application.MediatR.Vehicles.CreateVehicle
{
    public record CreateVehicleCommand(
        [Required] string PlateNumber,
        [Required] string Brand,
        [Required] string Model,
        [Required] int Year,
        [Required] int VehicleTypeId,
        [Required] int BookingValuePerDay
     ) : IRequest;
}
