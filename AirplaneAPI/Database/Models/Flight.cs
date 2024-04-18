using System.ComponentModel.DataAnnotations.Schema;

namespace AirplaneAPI.Database.Models;

public class Flight
{
    public int Id { get; set; }
    
    public int DepartureAirportId { get; set; } // Foreign Key
    public int ArrivalAirportId { get; set; } // Foreign Key
    public Airport DepartureAirport { get; set; } // Navigation Property
    public Airport ArrivalAirport { get; set; } // Navigation Property
    public Airplane Airplane { get; set; } // Navigation Property
    public DateTime DepartureTime { get; set; } // Time of departure
    public DateTime ArrivalTime { get; set; } // Time of arrival
    public string FlightNumber { get; set; } // Flight number
    public int Distance { get; set; } // Distance between the airports
    public int Duration { get; set; } // Duration of the flight
    public int FuelConsumption { get; set; } // Fuel consumption per hour
    public int FuelCost { get; set; } // Cost of fuel
    public int MaxSpeed { get; set; } // Maximum speed of the airplane
}