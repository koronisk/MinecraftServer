using MinecraftServer.Models;
using MongoDB.Driver;
using ZstdSharp.Unsafe;

namespace MinecraftServer.Services;

public class GameModeService
{
    private readonly Dictionary<string, GameMode> _gameModes;

    public GameModeService(IConfiguration config)
    {
        _gameModes = [];
    }

    public Dictionary<string, GameMode> GetAll() => _gameModes;
    public GameMode? Get(string id) => _gameModes.ContainsKey(id) ? _gameModes[id] : null;

    public void Sync(GameMode gameMode)
    {
        var lobbies = _gameModes[gameMode.Id].Lobbies;
        gameMode.Lobbies = lobbies;
        _gameModes[gameMode.Id] = gameMode;
    }
}