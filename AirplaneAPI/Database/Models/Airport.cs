using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using AirplaneAPI.Database.DTO;

namespace AirplaneAPI.Database.Models;

public class Airport : AirportDTO
{   
    public int Id { get; set; }
    [JsonIgnore]
    public List<Flight> Departures { get; set; }
    [JsonIgnore]
    public List<Flight> Arrivals { get; set; }
}