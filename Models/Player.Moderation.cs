namespace MinecraftServer.Models;

public partial class Player
{
    public void Ban(GameMode gameMode)
    {
        if (GameModes.TryGetValue(gameMode.Id, out var playerGameMode))
            playerGameMode.IsBanned = true;
    }
}