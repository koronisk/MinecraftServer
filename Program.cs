using MinecraftServer.Middleware;
using MinecraftServer.Routes;
using MinecraftServer.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<PlayerService>();

var app = builder.Build();

app.MapPlayerRoutes();

app.UseAccessKeyAuth();

app.Run();