using System.Text.Json.Serialization;
using AirplaneAPI.Database.DTO;

namespace AirplaneAPI.Database.Models;

public class Airplane : AirplaneDTO
{
    public int Id { get; set; }
    [JsonIgnore]
    public ICollection<Flight> Flights { get; set; }
}
