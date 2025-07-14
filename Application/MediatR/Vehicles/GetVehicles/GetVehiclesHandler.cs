using AutoMapper;
using Domain.Entities;
using Domain.Repositories;
using MediatR;

namespace Application.MediatR.Vehicles.GetVehicles
{
    public class GetVehiclesHandler : IRequestHandler<GetVehiclesQuery, IEnumerable<VehicleDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetVehiclesHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<VehicleDto>> Handle(GetVehiclesQuery request, CancellationToken cancellationToken)
        {
            var vehicles = await _unitOfWork.Repository<Vehicle>().GetAllAsync();
            return _mapper.Map<IEnumerable<VehicleDto>>(vehicles);
        }
    }
}
