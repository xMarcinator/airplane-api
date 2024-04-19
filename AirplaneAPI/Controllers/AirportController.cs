using AirplaneAPI.Database.DTO;
using AirplaneAPI.Database.Models;
using AirplaneAPI.Database.Repositories.Interface;
using AirplaneAPI.Simulation;
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
    public async Task<ActionResult<Airport>> CreateAirport(AirportDTO airport)
    {
        var model = await _airportRepo.CreateAsync(AirportDTO.ToAirport(airport));
        
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
    
    
    [HttpGet("{departureAirportId}/departures")]
    public async Task<ActionResult<IEnumerable<FlightProgress>>> GetDepartures(int departureAirportId)
    {
        var flights = await _airportRepo.GetDepartures(departureAirportId);

        if (flights == null)
        {
            return Problem(statusCode:StatusCodes.Status404NotFound, detail:"No Flights found");
        }
        
        return await GetFlightProgress(flights);
    }
    
    
    [HttpGet("{departureAirportId}/arrivals")]
    public async Task<ActionResult<IEnumerable<FlightProgress>>> GetArrivals(int departureAirportId)
    {
        var flights = await _airportRepo.GetArrivals(departureAirportId);

        if (flights == null)
        {
            return Problem(statusCode:StatusCodes.Status404NotFound, detail:"No Flights found");
        }
        
        return await GetFlightProgress(flights);
    }
    
    [HttpGet("{departureAirportId}/arrivals")]
    private Task<ActionResult<IEnumerable<FlightProgress>>> GetFlightProgress(IEnumerable<Flight> flights)
    {
        var enumerable = flights as Flight[] ?? flights.ToArray();
        if (!enumerable.Any())
        {
            return Task.FromResult<ActionResult<IEnumerable<FlightProgress>>>(Problem(statusCode:StatusCodes.Status404NotFound, detail:"No Flights found"));
        }

        return Task.FromResult<ActionResult<IEnumerable<FlightProgress>>>(enumerable.Select(FlightProgressUtils.GetFlightPosition).ToList());
    }
    
    
}