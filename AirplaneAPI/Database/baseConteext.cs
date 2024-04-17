using dotenv.net.Utilities;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Extensions;

namespace AirplaneAPI.Database;

public abstract class BaseContext : DbContext
{
    public string ConnectionString => _connectionString;
    private string _connectionString;

    protected BaseContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
        // create the database if it doesn't exist
        //Database.EnsureCreated();
        
        // Build the connection string
        _connectionString = buildConnectionString();
    }
    
    protected abstract void OnSeed(ModelBuilder modelBuilder);
    
    private string buildConnectionString ()
    {
        string prefix = "";
        
        var tyoe = this.GetType().Attributes.GetAttributeOfType<ContextInfo>();
        
        if (tyoe != null)
        {
            prefix = tyoe.EnvPrefix;
        }
        
        
        // build sql connection string
        var connectionBuilder = new SqlConnectionStringBuilder();
     
        // get the connection string from the environment
        if (EnvReader.TryGetStringValue(prefix + "DB_CONNECTION",out var connectionString))
            connectionBuilder.ConnectionString = connectionString;

        if (EnvReader.TryGetStringValue(prefix+"DB_ADDRESS", out var address))
        {
            if (EnvReader.TryGetStringValue(prefix+"DB_PORT", out var port))
            {
                //connectionBuilder.
                connectionBuilder.DataSource = $"{address},{port}";
            }
            else
            {
                connectionBuilder.DataSource = address;
            }
        }
        
        if (EnvReader.TryGetStringValue(prefix+"DB_USER",out var user))
            connectionBuilder.UserID = user; 
        
        if (EnvReader.TryGetStringValue(prefix+"DB_PASSWORD",out var pass))
            connectionBuilder.Password = pass;
         
        if (EnvReader.TryGetBooleanValue(prefix+"DB_TRUST",out var trust))
            connectionBuilder.TrustServerCertificate = trust;

        #if DEBUG
                Console.WriteLine("Connection String: " + connectionBuilder.ConnectionString);
        #endif

        return connectionBuilder.ConnectionString;
    }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(_connectionString);
    }
}

[AttributeUsage(AttributeTargets.Class)]
public class ContextInfo(string envPrefix) : Attribute
{
    public string EnvPrefix { get; set; } = envPrefix;
}