using AdPlatformService.Models;

namespace AdPlatformService.Services;

public class PlatformService
{
    private List<Platform> platforms = new();

    public void LoadPlatforms(IEnumerable<string> lines)
    {
        var newPlatforms = platforms;

        foreach (var line in lines)
        {
            if (string.IsNullOrWhiteSpace(line)) 
                continue;

            var parts = line.Split(':', 2);
            
            if (parts.Length != 2)
                continue;

            var name = parts[0].Trim();

            var locations = parts[1]
                .Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
                .ToList();

            newPlatforms.Add(new Platform
            {
                Name = name,
                Locations = locations
            });
        }

        platforms = newPlatforms;
    }

    public List<string> SearchPlatforms(string location)
    {
        return platforms
            .Where(p => p.Locations.Any(location.StartsWith))
            .Select(p => p.Name)
            .Distinct()
            .ToList();
    }
}