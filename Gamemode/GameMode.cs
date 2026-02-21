using System.Text.Json.Serialization;

namespace MinecraftServer;

public class GameMode
{
    public required string Id { get; init; }
    public required string Name { get; init; }

    public HashSet<Group> Groups { get; init; } = [];

    [JsonIgnore] public Dictionary<int, Lobby> Lobbies { get; set; } = new();

    public Lobby CreateLobby()
    {
        var id = GetNextFreeId();
        var lobby = new Lobby(this, Guid.NewGuid().ToString(), id);

        Lobbies[id] = lobby;

        return lobby;
    }

    private int GetNextFreeId()
    {
        if (Lobbies.Count == 0)
            return 0;

        var id = 0;

        while (Lobbies.ContainsKey(id))
        {
            id++;
        }

        return id;
    }

    public void RemoveLobby(Lobby lobby)
    {
        Lobbies.Remove(lobby.GameModeId);
    }
}