using AirplaneAPI.Database.Models;

namespace AirplaneAPI.Database.DTO;


public class AirplaneDTO
{
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


    public static Airplane ToAirplane(AirplaneDTO airplane) =>
        new()
        {
            MotorType = airplane.MotorType,
            MaxSpeed = airplane.MaxSpeed,
            FuelConsumption = airplane.FuelConsumption,
            FuelCapacity = airplane.FuelCapacity,
            PassangerCount = airplane.PassangerCount,
            PilotCount = airplane.PilotCount,
            PlaneType = airplane.PlaneType,
            Price = airplane.Price,
            Wingspan = airplane.Wingspan,
            Height = airplane.Height,
            Length = airplane.Length,
            MaxLiftOffWeight = airplane.MaxLiftOffWeight,
            PlaneWeight = airplane.PlaneWeight,
            Range = airplane.Range,
            Capacity = airplane.Capacity,
            Manufacturer = airplane.Manufacturer,
            Model = airplane.Model
        };
}

public enum PlaneType
{
    Glider, Private, Passenger, Transport, Fighter, Cargo, Other    
}

public enum MotorType
{
    Jet,Prop,Electric,Turboprop,Turbojet,Turbofan,Turboshaft,Other
}
