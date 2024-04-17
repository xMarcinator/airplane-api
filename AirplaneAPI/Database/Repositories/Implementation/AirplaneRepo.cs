using AirplaneAPI.Database.Models;
using AirplaneAPI.Database.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace AirplaneAPI.Database.Repositories.Implementation;

public class AirplaneRepo(ApplicationDbContext context) : IAirplaneRepo
{
    public async Task<IEnumerable<Airplane>> ReadAllAsync()
    {   
        return await context.Airplanes.ToListAsync();
    }

    public async Task<Airplane?> ReadAsync(int criteria)
    {
        return await context.Airplanes.FindAsync(criteria);
    }

    public async Task<Airplane> CreateAsync(Airplane entity)
    {
        context.Airplanes.Add(entity);
        await context.SaveChangesAsync();
        return entity;
    }

    public async Task<Airplane> UpdateAsync(Airplane entity)
    {
        context.Airplanes.Update(entity);
        await context.SaveChangesAsync();
        return entity;
    }

    public async Task<Airplane> DeleteAsync(Airplane entity)
    {
        context.Airplanes.Remove(entity);
        await context.SaveChangesAsync();
        return entity;
    }

    public async Task<Airplane?> DeleteAsync(int criteria)
    {
        var entity = await context.Airplanes.FindAsync(criteria);
        
        if (entity == null)
        {
            return null;
        }
        
        context.Airplanes.Remove(entity);
        await context.SaveChangesAsync();
        return entity;
    }
}