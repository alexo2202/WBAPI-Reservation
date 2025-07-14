using Application.MediatR.Vehicles;
using MediatR;

namespace Application.MediatR.VehicleTypes.GetVehicleTypes
{
    public record GetVehicleTypesQuery() : IRequest<IEnumerable<VehicleTypeDto>>;
}
