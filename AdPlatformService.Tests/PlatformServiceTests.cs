using AdPlatformService.Services;

namespace AdPlatformService.Tests;

public class PlatformServiceTests
{
    [Fact]
    public void LoadPlatforms_ShouldLoadCorrectly()
    {
        var service = new PlatformService();

        var lines = new[]
        {
            "Яндекс.Директ:/ru",
            "Ревдинский рабочий:/ru/svrd/revda,/ru/svrd/pervik",
            "Газета уральских москвичей:/ru/msk,/ru/permobl,/ru/chelobl",
            "Крутая реклама:/ru/svrd"
        };
        
        service.LoadPlatforms(lines);
        
        var result = service.SearchPlatforms("/ru/svrd/revda");

        Assert.Contains("Яндекс.Директ", result);
        Assert.Contains("Ревдинский рабочий", result);
        Assert.Contains("Крутая реклама", result);
        Assert.DoesNotContain("Газета уральских москвичей", result);
    }
    
    [Theory]
    [InlineData("/ru", new[] { "Яндекс.Директ" })]
    [InlineData("/ru/svrd", new[] { "Яндекс.Директ", "Крутая реклама" })]
    [InlineData("/ru/msk", new[] { "Яндекс.Директ", "Газета уральских москвичей" })]
    public void SearchPlatforms_ShouldReturnExpectedResults(string location, string[] expected)
    {
        var service = new PlatformService();
        service.LoadPlatforms(new[]
        {
            "Яндекс.Директ:/ru",
            "Крутая реклама:/ru/svrd",
            "Газета уральских москвичей:/ru/msk,/ru/permobl,/ru/chelobl"
        });

        var result = service.SearchPlatforms(location);

        foreach (var name in expected)
        {
            Assert.Contains(name, result);
        }
    }
    
    [Fact]
    public void LoadFromLines_ShouldIgnoreInvalidLines()
    {
        var service = new PlatformService();
        service.LoadPlatforms(new[]
        {
            "НевернаяСтрока",
            ":", // пустая строка
            "Пустой:/",
            "Корректная:/ru/svrd"
        });

        var result = service.SearchPlatforms("/ru/svrd");

        Assert.Contains("Корректная", result);
        Assert.DoesNotContain("НевернаяСтрока", result);
    }
}