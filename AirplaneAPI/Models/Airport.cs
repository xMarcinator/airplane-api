namespace AirplaneAPI.Models;

public class Airport
{   
    public int Id { get; set; }
    public string Name { get; set; }
    public string City { get; set; }
    public string Country { get; set; }
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    public double Altitude { get; set; }
    public double Timezone { get; set; }
    public string DST { get; set; }
    public string TzDatabaseTimeZone { get; set; }
}