using Domain.Entities;
using Domain.Repositories;
using MediatR;

namespace Application.MediatR.Vehicles.DeleteVehicle
{
    public class DeleteVehicleHandler : IRequestHandler<DeleteVehicleCommand>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteVehicleHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(DeleteVehicleCommand request, CancellationToken cancellationToken)
        {
            var repo = _unitOfWork.Repository<Vehicle>();
            var vehicle = await repo.GetByIdAsync(request.Id);
            if (vehicle == null)
                throw new KeyNotFoundException($"Vehicle with Id {request.Id} not found.");
            await repo.DeleteAsync(vehicle);
        }
    }
} 