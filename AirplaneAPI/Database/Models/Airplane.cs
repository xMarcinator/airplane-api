namespace AirplaneAPI.Database.Models;

public class Airplane
{
    public int Id { get; set; }
    public string Model { get; set; }
    public string Manufacturer { get; set; }
    
    public int Capacity { get; set; }
    public int Range { get; set; }
    public int PlaneWeight { get; set; }
    public int MaxLiftOffWeight { get; set; }
    public int Length { get; set; }
    public int Height { get; set; }
    public int Wingspan { get; set; }
    public int Price { get; set; }
    public MotorType MotorType { get; set; }
    public PlaneType PlaneType { get; set; }
    public int PilotCount { get; set; }
    public int PassangerCount { get; set; }
    public int FuelCapacity { get; set; }
    public int FuelConsumption { get; set; }
    public int MaxSpeed { get; set; }
    
    public List<Flight> Flights { get; set; }
}

public enum PlaneType
{
    Glider, Private, Passenger, Transport, Fighter, Cargo, Other    
}

public enum MotorType
{
    Jet,Prop,Electric,Turboprop,Turbojet,Turbofan,Turboshaft,Other
}

