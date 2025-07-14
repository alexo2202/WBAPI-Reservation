using Application.MediatR.Vehicles.CreateVehicle;
using AutoMapper;
using Domain.Entities;
using Domain.Repositories;
using Moq;

public class CreateVehicleHandlerTests
{
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly Mock<IMapper> _mapperMock;
    private readonly CreateVehicleHandler _handler;

    public CreateVehicleHandlerTests()
    {
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _mapperMock = new Mock<IMapper>();
        _handler = new CreateVehicleHandler(_unitOfWorkMock.Object, _mapperMock.Object);
    }

    [Fact]
    public async Task Handle_ShouldMapAndAddVehicle()
    {
        // Arrange
        var command = new CreateVehicleCommand(
            PlateNumber: "XYZ789",
            Brand: "Kia",
            Model: "Rio",
            Year: 2021,
            VehicleTypeId: 1,
            BookingValuePerDay: 90000
        );

        var vehicle = new Vehicle
        {
            PlateNumber = command.PlateNumber,
            Brand = command.Brand,
            Model = command.Model,
            Year = command.Year,
            VehicleTypeId = command.VehicleTypeId,
            BookingValuePerDay = command.BookingValuePerDay
        };

        var vehicleRepoMock = new Mock<IRepository<Vehicle>>();
        vehicleRepoMock.Setup(r => r.AddAsync(vehicle)).ReturnsAsync(vehicle);

        _unitOfWorkMock.Setup(u => u.Repository<Vehicle>()).Returns(vehicleRepoMock.Object);
        _mapperMock.Setup(m => m.Map<Vehicle>(command)).Returns(vehicle);

        // Act
        await _handler.Handle(command, CancellationToken.None);

        // Assert
        _mapperMock.Verify(m => m.Map<Vehicle>(command), Times.Once);
        vehicleRepoMock.Verify(r => r.AddAsync(It.Is<Vehicle>(v =>
            v.PlateNumber == command.PlateNumber &&
            v.Brand == command.Brand &&
            v.Model == command.Model &&
            v.Year == command.Year &&
            v.VehicleTypeId == command.VehicleTypeId &&
            v.BookingValuePerDay == command.BookingValuePerDay
        )), Times.Once);
    }
}
