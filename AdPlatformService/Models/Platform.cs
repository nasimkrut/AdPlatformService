namespace AdPlatformService.Models;

public class Platform
{
    public string Name { get; set; } = string.Empty;
    public List<string> Locations = new();
}