using Application.MediatR.Vehicles.GetVehicles;
using AutoMapper;
using Domain.Entities;
using Domain.Repositories;
using Moq;
using Xunit;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using Application.MediatR.Vehicles;

public class GetVehiclesHandlerTests
{
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly Mock<IMapper> _mapperMock;
    private readonly GetVehiclesHandler _handler;

    public GetVehiclesHandlerTests()
    {
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _mapperMock = new Mock<IMapper>();
        _handler = new GetVehiclesHandler(_unitOfWorkMock.Object, _mapperMock.Object);
    }

    [Fact]
    public async Task Handle_ShouldReturnMappedVehicleDtos_WhenVehiclesExist()
    {
        // Arrange
        var vehicles = new List<Vehicle>
        {
            new Vehicle
            {
                Id = 1,
                PlateNumber = "ABC123",
                Brand = "Toyota",
                Model = "Corolla",
                Year = 2020,
                VehicleTypeId = 2,
                BookingValuePerDay = 100000
            },
            new Vehicle
            {
                Id = 2,
                PlateNumber = "XYZ789",
                Brand = "Chevrolet",
                Model = "Spark",
                Year = 2019,
                VehicleTypeId = 3,
                BookingValuePerDay = 80000
            }
        };

        var vehicleDtos = new List<VehicleDto>
        {
            new VehicleDto
            {
                Id = 1,
                PlateNumber = "ABC123",
                Brand = "Toyota",
                Model = "Corolla",
                Year = 2020,
                VehicleTypeId = 2,
                BookingValuePerDay = 100000
            },
            new VehicleDto
            {
                Id = 2,
                PlateNumber = "XYZ789",
                Brand = "Chevrolet",
                Model = "Spark",
                Year = 2019,
                VehicleTypeId = 3,
                BookingValuePerDay = 80000
            }
        };

        var vehicleRepoMock = new Mock<IRepository<Vehicle>>();
        vehicleRepoMock.Setup(r => r.GetAllAsync()).ReturnsAsync(vehicles);
        _unitOfWorkMock.Setup(u => u.Repository<Vehicle>()).Returns(vehicleRepoMock.Object);
        _mapperMock.Setup(m => m.Map<IEnumerable<VehicleDto>>(vehicles)).Returns(vehicleDtos);

        var query = new GetVehiclesQuery();

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        result.Should().BeEquivalentTo(vehicleDtos);
    }

    [Fact]
    public async Task Handle_ShouldReturnEmptyList_WhenNoVehiclesExist()
    {
        // Arrange
        var vehicles = new List<Vehicle>();
        var vehicleDtos = new List<VehicleDto>();

        var vehicleRepoMock = new Mock<IRepository<Vehicle>>();
        vehicleRepoMock.Setup(r => r.GetAllAsync()).ReturnsAsync(vehicles);
        _unitOfWorkMock.Setup(u => u.Repository<Vehicle>()).Returns(vehicleRepoMock.Object);
        _mapperMock.Setup(m => m.Map<IEnumerable<VehicleDto>>(vehicles)).Returns(vehicleDtos);

        var query = new GetVehiclesQuery();

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        result.Should().BeEmpty();
    }
}
