using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MinecraftServer.Models;

public partial class Player
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }

    public string Name { get; set; } = null!;
    public string PasswordHash { get; set; } = null!;

    public bool IsBanned { get; set; } = false;
    public List<Group> Groups { get; set; } = [];

    [BsonIgnore] public string Playing { get; set; } = null!;

    public Dictionary<string, GameModePlayer> GameModes { get; set; } = [];

    public bool HasPlayed(GameMode gameMode)
    {
        return GameModes.ContainsKey(gameMode.Id);
    }
}