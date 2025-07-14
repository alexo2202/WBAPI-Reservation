using Application.MediatR.Vehicles.CreateVehicle;
using Application.MediatR.Vehicles.UpdateVehicle;
using AutoMapper;
using Domain.Entities;
namespace Application.MediatR.Vehicles
{
    public class VehicleProfile : Profile
    {
        public VehicleProfile()
        {
            CreateMap<Vehicle, VehicleDto>();
            CreateMap<CreateVehicleCommand, Vehicle>();
            CreateMap<UpdateVehicleCommand, Vehicle>();
        }
    }
}
