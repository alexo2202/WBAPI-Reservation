using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Application.MediatR.Vehicles.UpdateVehicle
{
    public record UpdateVehicleCommand(
        [Required] int Id,
        [Required] string PlateNumber,
        [Required] string Brand,
        [Required] string Model,
        [Required] int Year,
        [Required] int VehicleTypeId,
        [Required] int BookingValuePerDay
     ) : IRequest;
}
