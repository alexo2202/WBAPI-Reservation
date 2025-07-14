using Application.MediatR.Vehicles.DeleteVehicle;
using Domain.Entities;
using Domain.Repositories;
using Moq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using System;

namespace WBAPI_Reservation.Test
{
    public class DeleteVehicleHandlerTests
    {
        [Fact]
        public async Task Handle_ShouldDeleteVehicle_WhenVehicleExists()
        {
            // Arrange
            var mockRepo = new Mock<IRepository<Vehicle>>();
            var mockUow = new Mock<IUnitOfWork>();
            var vehicle = new Vehicle { Id = 1 };
            mockRepo.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(vehicle);
            mockRepo.Setup(r => r.DeleteAsync(vehicle)).Returns(Task.CompletedTask);
            mockUow.Setup(u => u.Repository<Vehicle>()).Returns(mockRepo.Object);
            var handler = new DeleteVehicleHandler(mockUow.Object);
            var command = new DeleteVehicleCommand(1);

            // Act
            await handler.Handle(command, CancellationToken.None);

            // Assert
            mockRepo.Verify(r => r.DeleteAsync(vehicle), Times.Once);
        }

        [Fact]
        public async Task Handle_ShouldThrowKeyNotFound_WhenVehicleDoesNotExist()
        {
            // Arrange
            var mockRepo = new Mock<IRepository<Vehicle>>();
            var mockUow = new Mock<IUnitOfWork>();
            mockRepo.Setup(r => r.GetByIdAsync(2)).ReturnsAsync((Vehicle)null);
            mockUow.Setup(u => u.Repository<Vehicle>()).Returns(mockRepo.Object);
            var handler = new DeleteVehicleHandler(mockUow.Object);
            var command = new DeleteVehicleCommand(2);

            // Act & Assert
            await Assert.ThrowsAsync<KeyNotFoundException>(() => handler.Handle(command, CancellationToken.None));
        }
    }
} 