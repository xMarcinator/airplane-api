namespace AirplaneAPI.Database.Models;

public class Flight
{
    public int Id { get; set; }
    public Airport DepartureAirport { get; set; }
    public Airport ArrivalAirport { get; set; }
    public Airplane Airplane { get; set; }
    public DateTime DepartureTime { get; set; }
    public DateTime ArrivalTime { get; set; }
    public string FlightNumber { get; set; }
    public int Distance { get; set; }
    public int Duration { get; set; }
    public int FuelConsumption { get; set; }
    public int FuelCost { get; set; }
    public int MaxSpeed { get; set; }
}