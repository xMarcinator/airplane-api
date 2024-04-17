using AirplaneAPI.Database;
using AirplaneAPI.Database.Models;
using AirplaneAPI.Database.Repositories.Interface;

namespace AirplaneAPI.Simulation;

public class FlightSimulator
{
    public List<FlightProgress> flightProgresses = new();
    private List<Flight> upcomingFlightslights = new();
    private readonly IFlightRepo _context;

    public FlightSimulator(IApplicationBuilder app)
    {
        var context = app.ApplicationServices.GetService<IFlightRepo>();
        
        if (context == null)
        {
            throw new Exception("Could not resolve ApplicationDbContext");
        }
        
        _context = context;
    }
    
    public async Task StartSimulation()
    {
        var flightsInProgressTask = _context.GetAllFlightsInProgress().ContinueWith((Task<IEnumerable<Flight>> r) =>
        {
            foreach (var flight in r.Result)
            {
                flightProgresses.Add(new FlightProgress(flight));
            }
        });
       
    }
}