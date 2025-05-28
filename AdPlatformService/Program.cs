using AdPlatformService.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<PlatformService>();
builder.Services.AddControllers();

var app = builder.Build();

app.UseRouting();
app.MapControllers();

app.Run();