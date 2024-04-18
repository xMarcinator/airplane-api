using AirplaneAPI.Database.Models;
using Microsoft.EntityFrameworkCore;

namespace AirplaneAPI.Database;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : BaseContext(options)
{
    public required DbSet<Models.Airplane> Airplanes { get; set; }
    public required DbSet<Models.Flight> Flights { get; set; }
    public required DbSet<Models.Airport> Airports { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Define the relationships between the models. Since entity framework can't handle the relationships between the models
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