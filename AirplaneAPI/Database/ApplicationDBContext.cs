using Microsoft.EntityFrameworkCore;

namespace AirplaneAPI.Database;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : BaseContext(options)
{
    public required DbSet<Models.Airplane> Airplanes { get; set; }
    public required DbSet<Models.Flight> Flights { get; set; }
    public required DbSet<Models.Airport> Airports { get; set; }
    protected override void OnSeed(ModelBuilder modelBuilder)
    {
        throw new NotImplementedException();
    }
}