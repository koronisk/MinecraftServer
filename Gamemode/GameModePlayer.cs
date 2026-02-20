using MongoDB.Bson.Serialization.Attributes;

namespace MinecraftServer;

public class GameModePlayer
{
    public string GameMode { get; set; } = null!;
    public bool IsBanned { get; set; } = false;
    [BsonIgnore] public bool IsOnline { get; set; } = false;
    
    public Dictionary<string, long> Balance { get; set; } = [];
    public Dictionary<string, string> Additional { get; set; } = [];
    public List<Group> Groups { get; set; } = [];
}