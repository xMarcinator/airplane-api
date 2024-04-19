using AirplaneAPI.Database;
using Microsoft.EntityFrameworkCore;

namespace AirplaneAPI.utils;

public static class AppSeed
{
    /// <summary>
    /// Seeds BaseContexts with initial data.
    /// </summary>
    /// <param name="app">Application builder to get contexts from</param>
    /// <param name="builderServices">Service collection to resolve basecontexts</param>
    public static async Task SeedContexts(this IApplicationBuilder app, IServiceCollection builderServices)
    {
        //gets a scope to resolve services
        using var scope = app.ApplicationServices.CreateScope();
        //gets a logger
        var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
        //gets all BaseContexts Types
        var contextTypes = builderServices
            .Where(sd => sd.ServiceType.IsAssignableTo(typeof(BaseContext)))
            .Select(sd => sd.ServiceType).ToList();
        
        //creates a task for each context
        var tasks = new Task[contextTypes.Count];
        
        for (int i = 0; i < tasks.Length; i++)
        {
            var contextType = contextTypes.ElementAt(i);
            var ctx = (BaseContext)scope.ServiceProvider
                .GetRequiredService(contextType);
            
            tasks[i] = SeedContext(ctx, logger);
        }

        await Task.WhenAll(tasks);

        //throw new NotSupportedException($"Type '{provider.GetType()}' is not supported!");

    }

    /// <summary>
    /// Seeds a BaseContext with initial data.
    /// </summary>
    /// <param name="ctx">Context to seed</param>
    /// <param name="logger">Logger used for logging</param>
    private static async Task SeedContext(BaseContext ctx, ILogger<Program> logger)
    {
        //migrates the context
        var migrated = await MigrateContext(ctx, logger);
        //if migrated, seeds the context. This naive approach assumes the database has no data.
        if (migrated)
        {
            logger.LogInformation("Seeding database {0} with initial data", ctx.GetType().Name);
            ctx.OnSeed(logger).Wait();
        }
    }
    
    /// <summary>
    /// Migrates a BaseContext.
    /// </summary>
    /// <param name="ctx">Context to migrate</param>
    /// <param name="logger">Logger used for logging</param>
    /// <returns>Indicates if context was migrated. If <c>true</c> it was migrated</returns>
    /// <exception cref="Exception"></exception>
    private static async Task<Boolean> MigrateContext(BaseContext ctx,ILogger<Program> logger)
    {
        //gets the database
        var db = ctx.Database;
        //gets the database name from the context
        var dbName = ctx.GetType().Name;

        logger.LogInformation($"Migrating database {dbName}...");
        //Waits for the database to be ready
        var retries = 10;
        var delayMs = 1000;

        while (!await db.CanConnectAsync() && retries-- > 0)
        {
            logger.LogInformation($"Database {dbName} not ready yet; waiting... (Attempts left: {retries})");
            Thread.Sleep(delayMs);
        }
        //if the database is not ready after 10 retries, throws an exception
        if (retries == 0)
        {
            logger.LogError($"Database {dbName} is not ready after 10 retries.");
            throw new Exception($"Database {dbName} is not ready after 10 retries.");
        }
        //if there are pending migrations, migrates the database
        if ((await db.GetPendingMigrationsAsync()).Any())
        {
            try
            {
                await db.MigrateAsync();
                logger.LogInformation($"Database {dbName} migrated successfully.");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"An error occurred while migrating the database {dbName}. Aborting seeding.");
                return false;
            }

            return true;
        }
        
        logger.LogInformation($"No pending migrations for {dbName}.");
        return false;
    }
}