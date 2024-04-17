using AirplaneAPI.Database.Models;
using AirplaneAPI.Database.Repositories.Interface;
using Microsoft.AspNetCore.Mvc;

namespace AirplaneAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class AirplaneController(IAirplaneRepo airplaneRepo) : Controller
{
    private IAirplaneRepo _airplaneRepo = airplaneRepo;

    //crud operations
    //get all airplanes
    [HttpGet("all")]
    public async Task<IEnumerable<Airplane>> GetAirplanes()
    {
        return await _airplaneRepo.ReadAllAsync();
    }
    
    //get airplane by id   
    [HttpGet("{id}")]
    public async Task<ActionResult<Airplane>> GetAirplane(int id)
    {
        Airplane? airplane = await _airplaneRepo.ReadAsync(id);
        
        if (airplane == null)
        {
            return Problem(statusCode:StatusCodes.Status404NotFound, detail:"Airplane not found");
        }
        
        return airplane;
    }
    
    //create airplane
    [HttpPost]
    public async Task<ActionResult<Airplane>> CreateAirplane(Airplane airplane)
    {
        var model = await _airplaneRepo.CreateAsync(airplane);
        
        return CreatedAtAction(nameof(GetAirplane), new {id = model.Id}, model);
    }
    
    //update airplane
    [HttpPut("{id}")]
    public async Task<ActionResult<Airplane>> UpdateAirplaneById(int id, Airplane airplane)
    {
        airplane.Id = id;
        var model = await _airplaneRepo.UpdateAsync(airplane);
        
        return CreatedAtAction(nameof(GetAirplane), new {id = model.Id}, model);
    }
    
    [HttpPut()]
    public async Task<ActionResult<Airplane>> UpdateAirplane(Airplane airplane)
    {
        var model = await _airplaneRepo.UpdateAsync(airplane);
        
        return CreatedAtAction(nameof(GetAirplane), new {id = model.Id}, model);
    }
    
    //delete airplane
    [HttpDelete("{id}")]
    public async Task<ActionResult<Airplane>> DeleteAirplane(int id)
    {
        var airplane = await _airplaneRepo.DeleteAsync(id);
        
        if (airplane == null)
        {
            return Problem(statusCode:StatusCodes.Status404NotFound, detail:"Airplane not found");
        }
        
        return airplane;
    }
    
    //delete airplane
    [HttpDelete]
    public async Task<ActionResult<Airplane>> DeleteAirplane(Airplane airplane)
    {
        var model = await _airplaneRepo.DeleteAsync(airplane);
        
        return model;
    }
    
    
}