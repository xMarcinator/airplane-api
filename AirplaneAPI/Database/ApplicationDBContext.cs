using AirplaneAPI.Database.DTO;
using AirplaneAPI.Database.Models;
using Microsoft.EntityFrameworkCore;

namespace AirplaneAPI.Database;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : BaseContext(options)
{
    public required DbSet<Models.Airplane> Airplanes { get; set; }
    public required DbSet<Models.Flight> Flights { get; set; }
    public required DbSet<Models.Airport> Airports { get; set; }

    public async override Task OnSeed(ILogger<Program> logger)
    {
        var Airplane = await Airplanes.AddAsync(new Airplane
        {
            Model = "Boeing 747",
            Manufacturer = "Boeing",
            Capacity = 416,
            Range = 14815,
            MaxSpeed = 988,
            PlaneWeight = 162400,
            MaxLiftOffWeight = 333400,
            Length = 70,
            Height = 19,
            Wingspan = 64,
            Price = 357000000,
            MotorType = MotorType.Jet,
            PlaneType = PlaneType.Passenger,
            PilotCount = 2,
            PassangerCount = 416,
            FuelCapacity = 183380,
            FuelConsumption = 12000
        });

        var firstAirport =await Airports.AddAsync(new Airport()
        {
            Name = "Schiphol",
            City = "Amsterdam",
            Country = "Netherlands",
            IATA = "AMS",
            Latitude = 52.308056,
            Longitude = 4.764167,
            Timezone = 1,
        });
        
        var secondAirport = await Airports.AddAsync(new Airport()
        {
            Name = "Heathrow",
            City = "London",
            Country = "United Kingdom",
            IATA = "LHR",
            Latitude = 51.4775,
            Longitude = -0.461389,
            Timezone = 0,
        });
        
        await Flights.AddAsync(new Flight()
        {
            DepartureAirport = firstAirport.Entity,
            ArrivalAirport = secondAirport.Entity,
            DepartureTime = DateTime.Now,
            ArrivalTime = DateTime.Now.AddHours(1),
            Airplane = Airplane.Entity,
            FlightNumber = "KL1234",
            Distance = 500,
            FuelCost = 1000
        });
        
        await SaveChangesAsync();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //Entity framework can't handle the relationships between the models with multiple relations to the same model, we have to define the relationships manually.
        modelBuilder.Entity<Airport>()
            .HasMany<Flight>(a=>a.Arrivals)
            .WithOne(f=>f.ArrivalAirport)
            .HasForeignKey(f=>f.ArrivalAirportId).OnDelete(DeleteBehavior.NoAction);
        
        modelBuilder.Entity<Airport>()
            .HasMany<Flight>(a=>a.Departures)
            .WithOne(f=>f.DepartureAirport)
            .HasForeignKey(f=>f.DepartureAirportId).OnDelete(DeleteBehavior.NoAction);
        
    }
}