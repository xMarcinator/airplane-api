using System.Reflection;
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
        _connectionString = BuildConnectionString();
    }

    /// <summary>
    ///  Seeds the database with initial data
    ///  This method is called after the database is migrated
    ///  Override this method to seed the database
    ///  </summary>
    public virtual Task OnSeed(ILogger<Program> logger)
    {
        logger.LogInformation("No Seeding for database {0}", GetType().Name);
        return Task.CompletedTask;
    }
    
    /// <summary>
    /// Builds the connection string from the environment
    /// </summary>
    /// <returns>Connection string</returns>
    private string BuildConnectionString ()
    {
        string prefix = "";
        
        // get Static context info from the class attribute
        var attribute = GetType().GetCustomAttributes<ContextInfo>(false).FirstOrDefault(); 

        if (attribute != null)
            prefix = attribute.EnvPrefix;

        
        // build sql connection string
        var connectionBuilder = new SqlConnectionStringBuilder();
     
        // get the connection string from the environment
        if (EnvReader.TryGetStringValue(prefix + "DB_CONNECTION",out var connectionString))
            connectionBuilder.ConnectionString = connectionString;

        // get the DB address from the environment
        if (EnvReader.TryGetStringValue(prefix+"DB_ADDRESS", out var address))
        {
            // get the DB port from the environment
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
        // get the DB user from the environment
        if (EnvReader.TryGetStringValue(prefix+"DB_USER",out var user))
            connectionBuilder.UserID = user; 
        // get the DB password from the environment
        if (EnvReader.TryGetStringValue(prefix+"DB_PASSWORD",out var pass))
            connectionBuilder.Password = pass;
         // get the DB trust from the environment
        if (EnvReader.TryGetBooleanValue(prefix+"DB_TRUST",out var trust))
            connectionBuilder.TrustServerCertificate = trust;
        // Log connection string in debug mode
        #if DEBUG
                Console.WriteLine("Connection String: " + connectionBuilder.ConnectionString);
        #endif

        return connectionBuilder.ConnectionString;
    }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        //user the connection string
        optionsBuilder.UseSqlServer(_connectionString);
    }
}