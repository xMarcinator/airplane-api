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
        modelBuilder.Entity<Flight>()
            .HasOne<Airport>(m => m.ArrivalAirport)
            .WithMany(m=>m.Arrivals)
            .HasForeignKey(m => m.ArrivalAirportId).OnDelete(DeleteBehavior.NoAction);
        modelBuilder.Entity<Flight>()
            .HasOne<Airport>(m => m.DepartureAirport)
            .WithMany(m=>m.Departures)
            .HasForeignKey(m => m.DepartureAirportId).OnDelete(DeleteBehavior.NoAction);
    }
}