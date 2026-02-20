namespace MinecraftServer;

public class Lobby
{
    public List<string> Players { get; }
    
    public GameMode GameMode { get; }
    public string Id { get; }
    public int GameModeId { get; }

    public Lobby(GameMode gameMode, string id, int gameModeId)
    {
        Players = [];
        
        GameMode = gameMode;
        Id = id;
        GameModeId = gameModeId;
    }

    void End() => GameMode.RemoveLobby(this);
    void Join(params List<int> players) => players.ForEach(player => Players.Add(player.ToString()));
}