using AirplaneAPI.Database.Models;

namespace AirplaneAPI.Database.DTO;


public class AirportDTO
{
    public string? ICAO { get; set; }
    public string Name { get; set; }
    public string City { get; set; }
    public string Country { get; set; }
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    public double Timezone { get; set; }
    
    public string? IATA { get; set; }
    
    public static Airport ToAirport(AirportDTO airplane) =>
        new()
        {
            Name = airplane.Name,
            City = airplane.City,
            Country = airplane.Country,
            Latitude = airplane.Latitude,
            Longitude = airplane.Longitude,
            Timezone = airplane.Timezone,
        };
}
