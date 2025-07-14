using AutoMapper;
using Domain.Entities;
using Domain.Repositories;
using MediatR;

namespace Application.MediatR.Vehicles.CreateVehicle
{
    public class CreateVehicleHandler : IRequestHandler<CreateVehicleCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateVehicleHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task Handle(CreateVehicleCommand request, CancellationToken cancellationToken)
        {
            var vehicle = _mapper.Map<Vehicle>(request);
            var data = await _unitOfWork.Repository<Vehicle>().AddAsync(vehicle);
        }
    }
}
