using AutoMapper;
using Domain.Entities;
using Domain.Repositories;
using MediatR;

namespace Application.MediatR.Vehicles.UpdateVehicle
{
    public class UpdateVehicleHandler : IRequestHandler<UpdateVehicleCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateVehicleHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task Handle(UpdateVehicleCommand request, CancellationToken cancellationToken)
        {
            var vehicle = _mapper.Map<Vehicle>(request);
            var data = await _unitOfWork.Repository<Vehicle>().UpdateAsync(vehicle);
        }
    }
}
