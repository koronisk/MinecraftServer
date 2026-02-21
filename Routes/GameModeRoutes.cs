using MinecraftServer.Models;
using MinecraftServer.Services;

namespace MinecraftServer.Routes;

public static class GameModeRoutes
{
    public record LobbyRequest
    {
        public required List<string> Players { get; init; }
    }

    public static void MapGameModeRoutes(this WebApplication app)
    {
        app.MapPost("/sync", (GameMode gameMode, GameModeService service) =>
        {
            service.Sync(gameMode);
            return Results.Ok();
        });

        app.MapPost("/{gameMode}/lobby/{lobby}/join", async (string gameMode, string lobby, LobbyRequest request,
            GameModeService gameModeService, PlayerService playerService) =>
        {
            var lobbyInstance = gameModeService.Get(gameMode)?.Lobbies.Values.FirstOrDefault(x => x.Id == lobby);
            if (lobbyInstance is null) return Results.NotFound();

            var players = new List<Player>();

            foreach (var x in request.Players)
            {
                var player = await playerService.GetByNameAsync(x);
                if (player is not null) players.Add(player);
            }

            lobbyInstance.Join(players);

            return Results.Ok();
        });

        app.MapPost("/{gameMode}/lobby/{lobby}/leave", async (string gameMode, string lobby, LobbyRequest request,
            GameModeService gameModeService, PlayerService playerService) =>
        {
            var lobbyInstance = gameModeService.Get(gameMode)?.Lobbies.Values.FirstOrDefault(x => x.Id == lobby);
            if (lobbyInstance is null) return Results.NotFound();

            var players = new List<Player>();

            foreach (var x in request.Players)
            {
                var player = await playerService.GetByNameAsync(x);
                if (player is not null) players.Add(player);
            }

            lobbyInstance.Leave(players);

            return Results.Ok();
        });

        app.MapPost("/{gameMode}/lobby/{lobby}/end", (string gameMode, string lobby,
            GameModeService gameModeService, PlayerService playerService) =>
        {
            var lobbyInstance = gameModeService.Get(gameMode)?.Lobbies.Values.FirstOrDefault(x => x.Id == lobby);
            if (lobbyInstance is null) return Results.NotFound();

            lobbyInstance.End();

            return Results.Ok();
        });
    }
}