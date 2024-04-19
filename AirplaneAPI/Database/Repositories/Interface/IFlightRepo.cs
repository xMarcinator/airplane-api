using AirplaneAPI.Database.Models;

namespace AirplaneAPI.Database.Repositories.Interface;

public interface IFlightRepo : IRepository<Flight,int>
{
    /// <summary>
    /// Get all flights in progress
    /// </summary>
    public Task<IEnumerable<Flight>> GetAllFlightsInProgress();
    
    /// <summary>
    /// Get all upcoming flights within a certain time frame
    /// <param name="lookAhead">Time to look ahead</param>
    /// </summary>
    /// 
    public Task<IEnumerable<Flight>> GetAllUpComingFlights(TimeSpan? lookAhead = null);
    /// <summary>
    /// Get all flights that have arrived
    /// </summary>
    /// <param name="airplaneId">ID of the Flight</param>
    /// <returns></returns>
    public Task<Flight?> GetFlightByIdWithAirports(int airplaneId);
}