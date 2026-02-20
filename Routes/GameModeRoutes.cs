using MinecraftServer.Models;
using MinecraftServer.Services;

namespace MinecraftServer.Routes;

public static class GameModeRoutes
{
    public static void MapGameModeRoutes(this WebApplication app)
    {
        app.MapPost("/players", async (Models.Player player, PlayerService service) =>
        {
            await service.CreateAsync(player);
            return Results.Ok(player);
        });

        app.MapGet("/players", async (PlayerService service) =>
        {
            var players = await service.GetAllAsync();
            return Results.Ok(players);
        });

        app.MapGet("/players/{name}", async (string name, PlayerService service) =>
        {
            var player = await service.GetByNameAsync(name);

            return player is null ? Results.NotFound() : Results.Ok(player);
        });
    }
}
