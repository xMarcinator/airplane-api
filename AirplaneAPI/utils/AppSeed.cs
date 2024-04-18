using AirplaneAPI.Database;
using Microsoft.EntityFrameworkCore;

namespace AirplaneAPI.utils;

public static class AppSeed
{
    public static async Task SeedContexts(this IApplicationBuilder app, IServiceCollection builderServices)
    {
        using var scope = app.ApplicationServices.CreateScope();
        var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
        
        var contextTypes = builderServices
            .Where(sd => sd.ServiceType.IsAssignableTo(typeof(BaseContext)))
            .Select(sd => sd.ServiceType).ToList();

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

    private static async Task SeedContext(BaseContext ctx, ILogger<Program> logger)
    {
        var migrated = await MigrateContext(ctx, logger);
        
        if (migrated)
        {
            logger.LogInformation("Seeding database {0} with initial data", ctx.GetType().Name);
            ctx.OnSeed(logger).Wait();
        }
    }
    
    private static async Task<Boolean> MigrateContext(BaseContext ctx,ILogger<Program> logger)
    {
        var db = ctx.Database;
        
        var dbName = ctx.GetType().Name;

        logger.LogInformation($"Migrating database {dbName}...");
        var retries = 10;
        var delayMs = 1000;

        while (!await db.CanConnectAsync() && retries-- > 0)
        {
            logger.LogInformation($"Database {dbName} not ready yet; waiting... (Attempts left: {retries})");
            Thread.Sleep(delayMs);
        }

        if (retries == 0)
        {
            logger.LogError($"Database {dbName} is not ready after 10 retries.");
            throw new Exception($"Database {dbName} is not ready after 10 retries.");
        }
        
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