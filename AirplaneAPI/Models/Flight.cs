namespace AirplaneAPI.Models;

public class Flight
{
    Airport DepartureAirport { get; set; }
    Airport ArrivalAirport { get; set; }
    Airplane Airplane { get; set; }
    public DateTime DepartureTime { get; set; }
    public DateTime ArrivalTime { get; set; }
    public int FlightNumber { get; set; }
    public int Distance { get; set; }
    public int Duration { get; set; }
    public int FuelConsumption { get; set; }
    public int FuelCost { get; set; }
}