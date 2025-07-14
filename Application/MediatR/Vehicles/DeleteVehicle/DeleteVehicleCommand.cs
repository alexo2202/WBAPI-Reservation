using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Application.MediatR.Vehicles.DeleteVehicle
{
    public record DeleteVehicleCommand(
        [Required] int Id
    ) : IRequest;
} 