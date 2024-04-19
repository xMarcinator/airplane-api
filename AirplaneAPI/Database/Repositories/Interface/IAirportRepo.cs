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

    /// <summary>
    /// Get an airport by its IATA code
    /// </summary>
    /// <param name="airportIata">IATA Code</param>
    /// <returns></returns>
    Task<Airport?> ReadIATAAsync(string airportIata);

    /// <summary>
    /// Get an airport by its ICAO code
    /// </summary>
    /// <param name="icao">ICAO code</param>
    /// <returns></returns>
    Task<Airport?> ReadICAOAsync(string icao);
}