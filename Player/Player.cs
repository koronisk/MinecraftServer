using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MinecraftServer.Models;

public partial class Player
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }

    public string Name { get; init; } = null!;
    public string PasswordHash { get; set; } = null!;

    public bool IsBanned { get; set; } = false;
    [BsonIgnore] public Lobby? Playing { get; set; }
    public List<Group> Groups { get; set; } = [];
    public Dictionary<string, GameModePlayer> GameModes { get; set; } = [];

    public bool HasPlayed(GameMode? gameMode)
    {
        if (gameMode is null) return false;
        return GameModes.ContainsKey(gameMode.Id);
    }
}