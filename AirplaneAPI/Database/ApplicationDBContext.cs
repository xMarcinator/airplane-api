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