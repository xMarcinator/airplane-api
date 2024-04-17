using AirplaneAPI.Database.Models;
using AirplaneAPI.Database.Repositories.Interface;
using Microsoft.AspNetCore.Mvc;

namespace AirplaneAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class AirportController(IAirportRepo airportRepo) : Controller
{
    private IAirportRepo _airportRepo = airportRepo;

    //crud operations
    //get all airports
    [HttpGet("all")]
    public async Task<IEnumerable<Airport>> GetAirports()
    {
        return await _airportRepo.ReadAllAsync();
    }
    
    //get airport by id   
    [HttpGet("{id}")]
    public async Task<ActionResult<Airport>> GetAirport(int id)
    {
        Airport? airport = await _airportRepo.ReadAsync(id);
        
        if (airport == null)
        {
            return Problem(statusCode:StatusCodes.Status404NotFound, detail:"Airport not found");
        }
        
        return airport;
    }
    
    //create airport
    [HttpPost]
    public async Task<ActionResult<Airport>> CreateAirport(Airport airport)
    {
        var model = await _airportRepo.CreateAsync(airport);
        
        return CreatedAtAction(nameof(GetAirport), new {id = model.Id}, model);
    }
    
    //update airport
    [HttpPut("{id}")]
    public async Task<ActionResult<Airport>> UpdateAirportById(int id, Airport airport)
    {
        airport.Id = id;
        var model = await _airportRepo.UpdateAsync(airport);
        
        return CreatedAtAction(nameof(GetAirport), new {id = model.Id}, model);
    }
    
    [HttpPut()]
    public async Task<ActionResult<Airport>> UpdateAirport(Airport airport)
    {
        var model = await _airportRepo.UpdateAsync(airport);
        
        return CreatedAtAction(nameof(GetAirport), new {id = model.Id}, model);
    }
    
    //delete airport
    [HttpDelete("{id}")]
    public async Task<ActionResult<Airport>> DeleteAirport(int id)
    {
        var airport = await _airportRepo.DeleteAsync(id);
        
        if (airport == null)
        {
            return Problem(statusCode:StatusCodes.Status404NotFound, detail:"Airport not found");
        }
        
        return airport;
    }
    
    //delete airport
    [HttpDelete]
    public async Task<ActionResult<Airport>> DeleteAirport(Airport airport)
    {
        var model = await _airportRepo.DeleteAsync(airport);
        
        return model;
    }
    
    
}