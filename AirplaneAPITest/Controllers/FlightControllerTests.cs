using AirplaneAPI.Controllers;
using AirplaneAPI.Database.Models;
using AirplaneAPI.Database.Repositories.Interface;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace AirplaneAPITest.Controllers;

public class FlightControllerTests
{
    [Fact]
    public async Task GetFlightProgress_FlightExists_ReturnsFlightProgress()
    {
        // Arrange
        var flightId = 1;
        var flight = new Flight
        {
            Id = flightId, DepartureTime = DateTime.Now.AddHours(-1), ArrivalTime = DateTime.Now.AddHours(1),
            ArrivalAirport = new Airport { Latitude = 34.0522, Longitude = -118.2437 },
            DepartureAirport = new Airport { Latitude = 40.7128, Longitude = -74.0060 }, Distance = 1000,
            FuelCost = 500
        };
        var mockFlightRepo = new Mock<IFlightRepo>();
        mockFlightRepo.Setup(repo => repo.GetFlightByIdWithAirports(flightId)).ReturnsAsync(flight);
        var controller = new FlightController(mockFlightRepo.Object);

        // Act
        var result = await controller.GetFlightProgress(flightId);

        // Assert
        var actionResult = Assert.IsType<ActionResult<FlightProgress>>(result);
        Assert.IsType<FlightProgress>(actionResult.Value);
    }

    [Fact]
    public async Task GetFlightProgress_FlightNotFound_ReturnsNotFound()
    {
        // Arrange
        var flightId = 1;
        Flight flight = null;
        var mockFlightRepo = new Mock<IFlightRepo>();
        mockFlightRepo.Setup(repo => repo.GetFlightByIdWithAirports(flightId)).ReturnsAsync(flight);
        var controller = new FlightController(mockFlightRepo.Object);

        // Act
        var res = await controller.GetFlightProgress(flightId);

        // Assert
        var actionResult = Assert.IsType<ObjectResult>(res.Result);
        Assert.IsType<ProblemDetails>(actionResult.Value);
    }

    // Additional tests for other controller methods can be written similarly
}