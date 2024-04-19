using AirplaneAPI.Database.Models;

namespace AirplaneAPI.Database.DTO;



public class FlightDTO
{
    public DateTime DepartureTime { get; set; } // Time of departure
    public DateTime ArrivalTime { get; set; } // Time of arrival
    public string FlightNumber { get; set; } // Flight number
    
    //This property is could be calculated from the airports' coordinates and would be redundant
    public int Distance { get; set; } // Distance between the airports
    public int FuelCost { get; set; } // Cost of fuel
    
    public TimeSpan Duration => ArrivalTime - DepartureTime; // Duration of the flight
    
    public static Flight ToAirport(FlightDTO flight) =>
        new()
        {
            DepartureTime = flight.DepartureTime,
            ArrivalTime = flight.ArrivalTime,
            FlightNumber = flight.FlightNumber,
            Distance = flight.Distance,
            FuelCost = flight.FuelCost,
        };
        
}