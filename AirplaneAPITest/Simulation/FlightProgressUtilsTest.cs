using AirplaneAPI.Database.Models;
using AirplaneAPI.Simulation;
using JetBrains.Annotations;

namespace AirplaneAPITest.Simulation;

[TestSubject(typeof(FlightProgressUtils))]
public class FlightProgressUtilsTest
{
    [Fact]
    public void GetFlightPosition_FlightHasNotDeparted_ReturnsCorrectProgress()
    {
        // Arrange
        var departureAirport = new Airport { Latitude = 40.7128, Longitude = -74.0060 };
        var flight = new Flight
        {
            DepartureTime = DateTime.Now.AddHours(1),
            Distance = 1000,
            DepartureAirport = departureAirport
        };

        // Act
        var progress = FlightProgressUtils.GetFlightPosition(flight);

        // Assert
        Assert.Equal(0, progress.TraveledDistance);
        Assert.Equal(0, progress.FuelConsumed);
        Assert.Equal(TimeSpan.Zero, progress.TimeElapsed);
        Assert.Equal(flight.Distance, progress.DistanceToDestination);
        Assert.Equal(departureAirport.Latitude, progress.CurrentLatitude);
        Assert.Equal(departureAirport.Longitude, progress.CurrentLongitude);
        Assert.Equal(FlightStatus.NotDeparted, progress.Status);
    }

    [Fact]
    public void GetFlightPosition_FlightHasAlreadyArrived_ReturnsCorrectProgress()
    {
        // Arrange
        var arrivalAirport = new Airport { Latitude = 34.0522, Longitude = -118.2437 };
        var flight = new Flight
        {
            DepartureTime = DateTime.Now.AddHours(-2),
            ArrivalTime = DateTime.Now.AddHours(-1),
            Distance = 1000,
            FuelCost = 500,
            ArrivalAirport = arrivalAirport
        };

        // Act
        var progress = FlightProgressUtils.GetFlightPosition(flight);

        // Assert
        Assert.Equal(flight.Distance, progress.TraveledDistance);
        Assert.Equal(flight.FuelCost, progress.FuelConsumed);
        Assert.Equal(flight.Duration, progress.TimeElapsed);
        Assert.Equal(0, progress.DistanceToDestination);
        Assert.Equal(arrivalAirport.Latitude, progress.CurrentLatitude);
        Assert.Equal(arrivalAirport.Longitude, progress.CurrentLongitude);
        Assert.Equal(FlightStatus.Arrived, progress.Status);
    }

    [Fact]
    public void GetFlightPosition_FlightInProgress_ReturnsCorrectProgress()
    {
        // Arrange
        var departureAirport = new Airport { Latitude = 40.7128, Longitude = -74.0060 };
        var arrivalAirport = new Airport { Latitude = 34.0522, Longitude = -118.2437 };
        var flight = new Flight
        {
            DepartureTime = DateTime.Now.AddHours(-2),
            ArrivalTime = DateTime.Now.AddHours(2),
            Distance = 3900,
            FuelCost = 1000,
            DepartureAirport = departureAirport,
            ArrivalAirport = arrivalAirport
        };

        // Act
        var progress = FlightProgressUtils.GetFlightPosition(flight);

        // Assert
        Assert.True(progress.TraveledDistance > 0);
        Assert.True(progress.FuelConsumed > 0);
        Assert.True(progress.TimeElapsed > TimeSpan.Zero);
        Assert.True(progress.DistanceToDestination > 0 && progress.DistanceToDestination < flight.Distance);
        Assert.NotEqual(departureAirport.Latitude, progress.CurrentLatitude);
        Assert.NotEqual(departureAirport.Longitude, progress.CurrentLongitude);
        Assert.NotEqual(arrivalAirport.Latitude, progress.CurrentLatitude);
        Assert.NotEqual(arrivalAirport.Longitude, progress.CurrentLongitude);
        Assert.NotEqual(FlightStatus.NotDeparted, progress.Status);
        Assert.NotEqual(FlightStatus.Arrived, progress.Status);
    }
}