namespace MinecraftServer;

public class GameMode
{
    private readonly Dictionary<int, Lobby> _lobbies;

    public GameMode()
    {
        _lobbies = [];
    }

    public string Id { get; }

    public Lobby CreateLobby()
    {
        var id = GetNextFreeId();
        var lobby = new Lobby(this, Guid.NewGuid().ToString(), id);
        
        _lobbies[id] = lobby;
        
        return lobby;
    }

    private int GetNextFreeId()
    {
        if (_lobbies.Count == 0)
            return 0;

        var id = 0;

        while (_lobbies.ContainsKey(id))
        {
            id++;
        }

        return id;
    }

    public void RemoveLobby(Lobby lobby)
    {
        _lobbies.Remove(lobby.GameModeId);
    }
}