using AirplaneAPI.Database.Models;
using AirplaneAPI.Database.Repositories.Interface;
using Microsoft.AspNetCore.Mvc;

namespace AirplaneAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class FlightController(IFlightRepo flightRepo) : Controller
{
    private IFlightRepo _flightRepo = flightRepo;

    //crud operations
    //get all flights
    [HttpGet("all")]
    public async Task<IEnumerable<Flight>> GetFlights()
    {
        return await _flightRepo.ReadAllAsync();
    }
    
    //get flight by id   
    [HttpGet("{id}")]
    public async Task<ActionResult<Flight>> GetFlight(int id)
    {
        Flight? flight = await _flightRepo.ReadAsync(id);
        
        if (flight == null)
        {
            return Problem(statusCode:StatusCodes.Status404NotFound, detail:"Flight not found");
        }
        
        return flight;
    }
    
    //create flight
    [HttpPost]
    public async Task<ActionResult<Flight>> CreateFlight(Flight flight)
    {
        var model = await _flightRepo.CreateAsync(flight);
        
        return CreatedAtAction(nameof(GetFlight), new {id = model.Id}, model);
    }
    
    //update flight
    [HttpPut("{id}")]
    public async Task<ActionResult<Flight>> UpdateFlightById(int id, Flight flight)
    {
        flight.Id = id;
        var model = await _flightRepo.UpdateAsync(flight);
        
        return CreatedAtAction(nameof(GetFlight), new {id = model.Id}, model);
    }
    
    [HttpPut()]
    public async Task<ActionResult<Flight>> UpdateFlight(Flight flight)
    {
        var model = await _flightRepo.UpdateAsync(flight);
        
        return CreatedAtAction(nameof(GetFlight), new {id = model.Id}, model);
    }
    
    //delete flight
    [HttpDelete("{id}")]
    public async Task<ActionResult<Flight>> DeleteFlight(int id)
    {
        var flight = await _flightRepo.DeleteAsync(id);
        
        if (flight == null)
        {
            return Problem(statusCode:StatusCodes.Status404NotFound, detail:"Flight not found");
        }
        
        return flight;
    }
    
    //delete flight
    [HttpDelete]
    public async Task<ActionResult<Flight>> DeleteFlight(Flight flight)
    {
        var model = await _flightRepo.DeleteAsync(flight);
        
        return model;
    }
    
    
}