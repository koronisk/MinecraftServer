using MongoDB.Bson.Serialization.Attributes;

namespace MinecraftServer;

public class GameModePlayer
{
    public required string GameMode { get; init; } = null!;
    
    public bool IsBanned { get; set; }
    public Dictionary<string, long> Balance { get; set; } = [];
    public Dictionary<string, string> Additional { get; set; } = [];
    public List<Group> Groups { get; set; } = [];
}