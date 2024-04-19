using AirplaneAPI.Database.DTO;
using AirplaneAPI.Database.Models;
using AirplaneAPI.Database.Repositories.Interface;
using AirplaneAPI.utils.json;
using dotenv.net.Utilities;

namespace AirplaneAPI.utils;
using System.Net.Http.Headers;

public class AirportManager
{
    public static async Task<Airport> GetOrImportAirport(IAirportRepo repo, string? iata = null, string? icao = null)
    {
        // Import airports from the airports.csv file
        
        //fetch the airport from the database
        
        Airport? fetchedAirport = null;
        if (iata != null)
        {
            fetchedAirport = await repo.ReadIATAAsync(iata);
        }
        else if (icao != null)
        {
            fetchedAirport = await repo.ReadICAOAsync(icao);
        }
        else
        {
            throw new Exception("No airport code provided");
        }
        
        if (fetchedAirport != null)
           return fetchedAirport;

        
        var newAirport = await FetchAirport(iata, icao);
        await repo.CreateAsync(newAirport);
        return newAirport;
    }
    
    private static async Task<Airport> FetchAirport(string? iata = null, string? icao = null)
    {
        var client = new HttpClient();
        var request = new HttpRequestMessage
        {
            Method = HttpMethod.Get,
            RequestUri = new Uri($"https://airport-info.p.rapidapi.com/airport?iata={iata}&icao={icao}"),
            Headers =
            {
                { "X-RapidAPI-Key", EnvReader.GetStringValue("RAPID_API_KEY") },
                { "X-RapidAPI-Host", "airport-info.p.rapidapi.com" },
            },
        };
        using (var response = await client.SendAsync(request))
        {
            response.EnsureSuccessStatusCode();
            AirportInfoJson? body = await response.Content.ReadFromJsonAsync<AirportInfoJson>();
            
            if (body == null)
            {
                throw new Exception("Failed to fetch airport");
            }
            
            return new Airport
            {
                IATA = body.Iata,
                Name = body.Name,
                City = body.City,
                Country = body.Country,
                Latitude = body.Latitude,
                Longitude = body.Longitude,
            };
        }
    }
}