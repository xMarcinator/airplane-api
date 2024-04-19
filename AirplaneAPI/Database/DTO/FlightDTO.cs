using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using AirplaneAPI.Database.Models;

namespace AirplaneAPI.Database.DTO;



public class FlightDTO
{
    public DateTime DepartureTime { get; set; } // Time of departure
    public DateTime ArrivalTime { get; set; } // Time of arrival
    public string FlightNumber { get; set; } // Flight number
    public double Distance { get; set; } // Distance for the flight in km
    public int FuelCost { get; set; } // Cost of fuel
    
    public int DepartureAirportId { get; set; } // Foreign Key
    public int ArrivalAirportId { get; set; } // Foreign Key
    public int AirplaneId { get; set; } // Foreign Key
    
    [JsonIgnore]
    public TimeSpan Duration => ArrivalTime - DepartureTime; // Duration of the flight
    
    //TODO: Move to seperate class to avoid database entry for IATA
    [NotMapped]
    public string IATA { get; set; } // IATA code of the departure airport
    
    
    public static Flight ToAirport(FlightDTO flight) =>
        new()
        {
            DepartureTime = flight.DepartureTime,
            ArrivalTime = flight.ArrivalTime,
            FlightNumber = flight.FlightNumber,
            Distance = flight.Distance,
            FuelCost = flight.FuelCost,
            AirplaneId = flight.AirplaneId,
        };
        
}
