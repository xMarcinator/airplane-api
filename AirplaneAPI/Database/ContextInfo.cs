namespace AirplaneAPI.Database;

[AttributeUsage(AttributeTargets.Class)]
public class ContextInfo(string envPrefix) : Attribute
{
    public string EnvPrefix { get; set; } = envPrefix;
}