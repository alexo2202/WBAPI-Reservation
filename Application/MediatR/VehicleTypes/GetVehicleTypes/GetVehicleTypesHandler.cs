using AutoMapper;
using Domain.Entities;
using Domain.Repositories;
using MediatR;

namespace Application.MediatR.VehicleTypes.GetVehicleTypes
{
    public class GetVehicleTypesHandler : IRequestHandler<GetVehicleTypesQuery, IEnumerable<VehicleTypeDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetVehicleTypesHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<IEnumerable<VehicleTypeDto>> Handle(GetVehicleTypesQuery request, CancellationToken cancellationToken)
        {
            var vehicleTypes = await _unitOfWork.Repository<VehicleType>().GetAllAsync();
            return _mapper.Map<IEnumerable<VehicleTypeDto>>(vehicleTypes);
        }
    }
}
