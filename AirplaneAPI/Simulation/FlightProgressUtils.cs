using System.Drawing;
using AirplaneAPI.Database.Models;

namespace AirplaneAPI.Simulation;

public static class FlightProgressUtils
{
    /// <summary>
    /// Calculate the position of the Flight progress based on the time elapsed
    /// </summary>
    /// <param name="flight">Object of the flight being fetched for progress</param>
    /// <returns></returns>
    public static FlightProgress GetFlightPosition(Flight flight)
    {
        // Check if the flight has not yet departed
        if (flight.DepartureTime > DateTime.Now)
        {
            // The flight has not yet departed
            return new FlightProgress()
            {
                TraveledDistance = 0,
                FuelConsumed = 0,
                TimeElapsed = TimeSpan.Zero,
                DistanceToDestination = flight.Distance,
                Status = FlightStatus.NotDeparted
            };
        }
        
        // Check if the flight has already arrived
        if (flight.ArrivalTime < DateTime.Now)
        {
            // The flight has already arrived
            return new FlightProgress()
            {
                TraveledDistance = flight.Distance,
                FuelConsumed = flight.FuelCost,
                TimeElapsed = flight.Duration,
                DistanceToDestination = 0,
                Status = FlightStatus.Arrived
            };
        }
        
        
        // Calculate the position of the airplane based on the progress of the time
        var timeElapsed = DateTime.Now - flight.DepartureTime;
       
        // Calculate the percentage of the flight that has been completed
        double percent = timeElapsed / flight.Duration;
        
        // Calculate the position of the airplane based on the distance traveled
        var dx = flight.ArrivalAirport.Latitude - flight.DepartureAirport.Latitude;
        var dy = flight.ArrivalAirport.Longitude - flight.DepartureAirport.Longitude;
        // Calculate the current position of the airplane
        var x = flight.DepartureAirport.Latitude + dx * percent;
        var y = flight.DepartureAirport.Longitude + dy * percent;
        
        var distance = GetDistanceFromLatLonInKm(flight.DepartureAirport.Latitude, flight.DepartureAirport.Longitude, flight.ArrivalAirport.Latitude, flight.ArrivalAirport.Longitude);
        
        var distanceTraveled = distance * percent;

        return new FlightProgress
        {
            FuelConsumed = (int) (flight.FuelCost * percent),
            DistanceToDestination = distance - distanceTraveled,
            TraveledDistance = distanceTraveled,
            TimeElapsed = timeElapsed,
            CurrentLatitude = x,
            CurrentLongitude = y
        };
    }
    
    /// <summary>
    /// Calculate the distance between two points on the Earth's surface
    /// see https://stackoverflow.com/questions/27928/calculate-distance-between-two-latitude-longitude-points-haversine-formula
    /// </summary>
    public static double GetDistanceFromLatLonInKm(double lat1,double lon1,double lat2,double lon2) {
        const int r = 6371; // Radius of the earth in km
        var dLat = Deg2Rad(lat2-lat1);  // deg2rad below
        var dLon = Deg2Rad(lon2-lon1); 
        var a = 
                Math.Sin(dLat/2) * Math.Sin(dLat/2) +
                Math.Cos(Deg2Rad(lat1)) * Math.Cos(Deg2Rad(lat2)) * 
                Math.Sin(dLon/2) * Math.Sin(dLon/2); 
        var c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1-a)); 
        var d = r * c; // Distance in km
        return d;
    }

    /// <summary>
    /// Convert degrees to radians
    /// </summary>
    public static double Deg2Rad(double deg)
    {
        return deg * (Math.PI / 180);
    }
}