using AirplaneAPI.Database.Models;
using AirplaneAPI.Database.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace AirplaneAPI.Database.Repositories.Implementation;

public class FlightRepo : IFlightRepo
{
    private readonly ApplicationDbContext _context;

    public FlightRepo(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Flight>> ReadAllAsync()
    {   
        return await _context.Flights.ToListAsync();
    }
    
    public async Task<Flight?> ReadAsync(int criteria)
    {
        return await _context.Flights.FindAsync(criteria);
    }
    
    public async Task<Flight> CreateAsync(Flight entity)
    {
        _context.Flights.Add(entity);
        await _context.SaveChangesAsync();
        return entity;
    }
    
    public async Task<Flight> UpdateAsync(Flight entity)
    {
        _context.Flights.Update(entity);
        await _context.SaveChangesAsync();
        return entity;
    }
    
    public async Task<Flight> DeleteAsync(Flight entity)
    {
        _context.Flights.Remove(entity);
        await _context.SaveChangesAsync();
        return entity;
    }
    
    public async Task<Flight?> DeleteAsync(int criteria)
    {
        var entity = await _context.Flights.FindAsync(criteria);
        
        if (entity == null)
        {
            return null;
        }
        
        _context.Flights.Remove(entity);
        await _context.SaveChangesAsync();
        return entity;
    }
    
    public async Task<IEnumerable<Flight>> GetAllFlightsInProgress()
    {
        var now = DateTime.Now;
        // Get all flights that have departed but not arrived
        return await _context.Flights
            .Where(f => f.DepartureTime < now && f.ArrivalTime > now).Include(f=>f.Airplane).ToListAsync();
    }

    public async Task<IEnumerable<Flight>> GetAllUpComingFlights(TimeSpan? lookAhead = null)
    {
        // Default to 1 hour
        if (lookAhead == null)
        {
            lookAhead = TimeSpan.FromHours(1);
        }
        // Get all flights that are departing within the look ahead time
        var limit = DateTime.Now.Add(lookAhead.Value);
        var now = DateTime.Now;

        return await _context.Flights
            .Where(f =>f.DepartureTime > now && f.DepartureTime < limit).Include(f=>f.Airplane).ToListAsync();
    }

    public async Task<Flight?> GetFlightByIdWithAirports(int airplaneId)
    {
        // Get the flight with the specified ID and include the departure and arrival airports
        return await _context.Flights
            .Include(f=>f.DepartureAirport)
            .Include(f=>f.ArrivalAirport)
            .FirstOrDefaultAsync(f => f.Id == airplaneId);
    }
}