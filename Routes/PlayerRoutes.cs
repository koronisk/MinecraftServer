using Microsoft.AspNetCore.Mvc;
using MinecraftServer.Models;
using MinecraftServer.Services;

namespace MinecraftServer.Routes;

public record PlayerRequest
{
    public required string Name { get; init; }
    public required string Password { get; init; }
}

public record PlayerResponse
{
    public record PlayerResponseGroup
    {
        public required string Prefix { get; init; }
        public required string Suffix { get; init; }
        public required string Color { get; init; }
    }

    public required string Name { get; init; }
    public required HashSet<string> Permissions { get; init; }
    public required string Playing { get; init; }
    public required bool IsBanned { get; init; }
    public required PlayerResponseGroup Group { get; init; }
    public PlayerResponseGroup? GameModeGroup { get; init; }
    public required Dictionary<string, GameModePlayer> GameModes { get; init; }
}

public static class PlayerRoutes
{
    public static void MapPlayerRoutes(this WebApplication app)
    {
        app.MapPost("/players", async (PlayerRequest request, PlayerService service) =>
        {
            var player = new Player
            {
                Name = request.Name,
                PasswordHash = PasswordHasher.HashPassword(request.Password)
            };

            await service.CreateAsync(player);
            return Results.Ok(player);
        });

        app.MapGet("/players/{name}", async (string name, [FromQuery] string? gameMode, PlayerService service,
            GameModeService gameModeService) =>
        {
            var player = await service.GetByNameAsync(name);
            if (player is null) return Results.NotFound();

            var permissions = new HashSet<string>();

            PlayerResponse.PlayerResponseGroup GetGroup(IEnumerable<Group> groups)
            {
                var prefix = string.Empty;
                var suffix = string.Empty;
                var color = string.Empty;

                var ordered = groups.OrderBy(x => x.Weight);
                foreach (var g in ordered)
                {
                    permissions.UnionWith(g.Permissions);

                    if (!string.IsNullOrEmpty(g.Prefix)) prefix = g.Prefix;
                    if (!string.IsNullOrEmpty(g.Suffix)) suffix = g.Suffix;
                    if (!string.IsNullOrEmpty(g.Color)) color = g.Color;
                }

                return new PlayerResponse.PlayerResponseGroup
                {
                    Prefix = prefix,
                    Suffix = suffix,
                    Color = color
                };
            }

            var mainGroup = GetGroup(player.Groups);

            PlayerResponse.PlayerResponseGroup? gameModeGroup = null;
            if (gameMode is not null && player.HasPlayed(gameModeService.Get(gameMode)))
            {
                gameModeGroup = GetGroup(player.GameModes[gameMode].Groups);
            }

            var response = new PlayerResponse
            {
                Name = player.Name,
                Permissions = permissions,
                Playing = player.Playing?.Id ?? string.Empty,
                IsBanned = player.IsBanned,
                Group = mainGroup,
                GameModeGroup = gameModeGroup,
                GameModes = player.GameModes
            };

            return Results.Ok(response);
        });
    }
}