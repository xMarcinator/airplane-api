using AirplaneAPI.Database.Models;

namespace AirplaneAPI.Database.Repositories.Interface;

public interface IAirportRepo : IRepository<Airport,int>
{
    /// <summary>
    /// Get all flights that have departed from a certain airport
    /// </summary>
    /// <param name="departureAirportId">Airport ID</param>
    /// <returns></returns>
    Task<IEnumerable<Flight>?> GetDepartures(int departureAirportId);
    /// <summary>
    /// Get all flights that have arrived at a certain airport
    /// </summary>
    /// <param name="departureAirportId">Airport ID</param>
    /// <returns></returns>
    Task<IEnumerable<Flight>?> GetArrivals(int departureAirportId);
}