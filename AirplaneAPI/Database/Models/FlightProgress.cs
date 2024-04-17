namespace AirplaneAPI.Database.Models;

public class FlightProgress(Flight flight)
{
    public Flight Flight { get; set; } = flight;

    public int TraveledDistance { get; set; } = 0;
    public int FuelConsumed { get; set; } = 0;
    public int TimeElapsed { get; set; } = 0;
    public int DistanceToDestination { get; set; } = 0;
}