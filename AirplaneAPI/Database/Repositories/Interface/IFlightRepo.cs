using AirplaneAPI.Database.Models;

namespace AirplaneAPI.Database.Repositories.Interface;

public interface IFlightRepo : IRepository<Flight,int>
{
    public Task<IEnumerable<Flight>> GetAllFlightsInProgress();
    public Task<IEnumerable<Flight>> GetAllUpComingFlights(TimeSpan? lookAhead = null);
}