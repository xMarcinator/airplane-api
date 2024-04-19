namespace AirplaneAPI.Database.Models;

public class FlightProgress
{
    public double TraveledDistance { get; set; } = 0;
    public double FuelConsumed { get; set; } = 0;
    public TimeSpan TimeElapsed { get; set; } = TimeSpan.Zero;
    public double DistanceToDestination { get; set; } = 0;
    public double CurrentLatitude { get; set; } = 0;
    public double CurrentLongitude { get; set; } = 0;
    
    public FlightStatus Status { get; set; } = FlightStatus.InProgress;
}

public enum FlightStatus
{
    NotDeparted,
    InProgress,
    Arrived,
    Canceled
}