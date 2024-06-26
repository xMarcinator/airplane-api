using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using AirplaneAPI.Database.DTO;

namespace AirplaneAPI.Database.Models;

public class Flight : FlightDTO
{
    public int Id { get; set; }
    [JsonIgnore]
    public Airport DepartureAirport { get; set; } // Navigation Property
    [JsonIgnore]
    public Airport ArrivalAirport { get; set; } // Navigation Property
    [JsonIgnore]
    public Airplane Airplane { get; set; } // Navigation Property
}