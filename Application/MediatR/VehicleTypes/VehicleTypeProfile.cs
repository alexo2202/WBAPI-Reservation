using AutoMapper;
using Domain.Entities;

namespace Application.MediatR.VehicleTypes
{
    class VehicleTypeProfile: Profile
    {
        public VehicleTypeProfile()
        {
            CreateMap<VehicleType, VehicleTypeDto>();
        }
    }
}
