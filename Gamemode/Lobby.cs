using MinecraftServer.Models;

namespace MinecraftServer;

public class Lobby
{
    public string Id { get; }
    public List<Player> Players { get; }

    public GameMode GameMode { get; }
    public int GameModeId { get; }

    public Lobby(GameMode gameMode, string id, int gameModeId)
    {
        Players = [];

        GameMode = gameMode;
        Id = id;
        GameModeId = gameModeId;
    }

    public void End()
    {
        Leave(Players);
        GameMode.RemoveLobby(this);
    }

    public void Leave(List<Player> players)
    {
        players.ForEach(player =>
        {
            player.Playing = this;
            Players.Remove(player);
        });
    }

    public void Join(List<Player> players)
    {
        players.ForEach(player =>
        {
            if (!player.HasPlayed(GameMode))
                player.GameModes.Add(GameMode.Name, new GameModePlayer { GameMode = GameMode.Name });

            player.Playing = this;
            Players.Add(player);
        });
    }
}