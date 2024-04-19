using AirplaneAPI.Database.Models;
using AirplaneAPI.Database.Repositories.Interface;
using AirplaneAPI.Simulation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AirplaneAPI.Database.Repositories.Implementation;

public class AirportRepo(ApplicationDbContext context)  : IAirportRepo
{
    public async Task<IEnumerable<Airport>> ReadAllAsync()
    {   
        return await context.Airports.ToListAsync();
    }

    public async Task<Airport?> ReadAsync(int criteria)
    {
        return await context.Airports.FindAsync(criteria);
    }

    public async Task<Airport> CreateAsync(Airport entity)
    {
        context.Airports.Add(entity);
        await context.SaveChangesAsync();
        return entity;
    }

    public async Task<Airport> UpdateAsync(Airport entity)
    {
        context.Airports.Update(entity);
        await context.SaveChangesAsync();
        return entity;
    }

    public async Task<Airport> DeleteAsync(Airport entity)
    {
        context.Airports.Remove(entity);
        await context.SaveChangesAsync();
        return entity;
    }

    public async Task<Airport?> DeleteAsync(int criteria)
    {
        var entity = await context.Airports.FindAsync(criteria);
        
        if (entity == null)
        {
            return null;
        }
        
        context.Airports.Remove(entity);
        await context.SaveChangesAsync();
        return entity;
    }

    public async Task<IEnumerable<Flight>?> GetDepartures(int departureAirportId)
    {
        // Include the Departures navigation property
        var entity = await context.Airports.Include(a => a.Departures).FirstOrDefaultAsync(a=>a.Id == departureAirportId);
        // If the entity is null, indicate no airport was found. Redundant check, but allow for future changes
        if (entity == null)
        {
            return null;
        }
        
        return entity.Departures;
    }
    
    public async Task<IEnumerable<Flight>?> GetArrivals(int arrivalAirportId)
    {
        var entity = await context.Airports.Include(a => a.Arrivals).FirstOrDefaultAsync(a=>a.Id == arrivalAirportId);
        // If the entity is null, indicate no airport was found. Redundant check, but allow for future changes
        if (entity == null)
        {
            return null;
        }
        
        return entity.Arrivals;
    }

    public async Task<Airport?> ReadIATAAsync(string airportIata)
    {
        var entity = await context.Airports.Include(a => a.Arrivals).FirstOrDefaultAsync(a=>a.IATA == airportIata);
        // If the entity is null, indicate no airport was found. Redundant check, but allow for future changes
        if (entity == null)
        {
            return null;
        }
        
        return entity;
    }

    public async Task<Airport?> ReadICAOAsync(string icao)
    {
        var entity = await context.Airports.Include(a => a.Arrivals).FirstOrDefaultAsync(a=>a.ICAO == icao);
        // If the entity is null, indicate no airport was found. Redundant check, but allow for future changes
        if (entity == null)
        {
            return null;
        }
        
        return entity;
    }
}