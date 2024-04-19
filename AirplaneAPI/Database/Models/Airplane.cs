using System.Text.Json.Serialization;
using AirplaneAPI.Database.DTO;

namespace AirplaneAPI.Database.Models;

public class Airplane : AirplaneDTO
{
    public int Id { get; set; }
    [JsonIgnore]
    public ICollection<Flight> Flights { get; set; }
}

public enum PlaneType
{
    Glider, Private, Passenger, Transport, Fighter, Cargo, Other    
}

public enum MotorType
{
    Jet,Prop,Electric,Turboprop,Turbojet,Turbofan,Turboshaft,Other
}

