using AirplaneAPI.Database.Models;
using AirplaneAPI.Database.Repositories.Interface;
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
}