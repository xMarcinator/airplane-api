using AirplaneAPI.Database.DTO;
using AirplaneAPI.Database.Models;
using AirplaneAPI.Database.Repositories.Interface;
using AirplaneAPI.Simulation;
using AirplaneAPI.utils;
using Microsoft.AspNetCore.Mvc;

namespace AirplaneAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class FlightController(IFlightRepo flightRepo) : Controller
{
    private IFlightRepo _flightRepo = flightRepo;
    
    [HttpGet("all")]
    public async Task<IEnumerable<Flight>> GetFlights()
    {
        return await _flightRepo.ReadAllAsync();
    }
    
    [HttpGet("{flightId}/progress")]
    public async Task<ActionResult<FlightProgress>> GetFlightProgress(int flightId)
    {
        // Get the flight with the airports included
        var flight = await _flightRepo.GetFlightByIdWithAirports(flightId);
        // If the flight is null, return a problem
        if (flight == null)
        {
            return Problem(statusCode:StatusCodes.Status404NotFound, detail:"Flight not found");
        }
        // Return the flight progress
        return FlightProgressUtils.GetFlightPosition(flight);
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
    public async Task<ActionResult<Flight>> CreateFlight(FlightDTO flight)
    {
        var model = await _flightRepo.CreateAsync(FlightDTO.ToAirport(flight));
        
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
}