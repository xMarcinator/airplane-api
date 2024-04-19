using AirplaneAPI.Database.Models;

namespace AirplaneAPI.Database.DTO;


public class AirportDTO
{   
    public string Name { get; set; }
    public string City { get; set; }
    public string Country { get; set; }
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    public double Altitude { get; set; }
    public double Timezone { get; set; }
    public string DST { get; set; }
    public string TzDatabaseTimeZone { get; set; }
    
    public static Airport ToAirport(AirportDTO airplane) =>
        new()
        {
            Name = airplane.Name,
            City = airplane.City,
            Country = airplane.Country,
            Latitude = airplane.Latitude,
            Longitude = airplane.Longitude,
            Altitude = airplane.Altitude,
            Timezone = airplane.Timezone,
            DST = airplane.DST,
            TzDatabaseTimeZone = airplane.TzDatabaseTimeZone
        };
}
