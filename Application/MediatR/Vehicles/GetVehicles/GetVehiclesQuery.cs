using MediatR;

namespace Application.MediatR.Vehicles.GetVehicles
{
    public record GetVehiclesQuery() : IRequest<IEnumerable<VehicleDto>>;
}
