namespace MinecraftServer.Models;

public enum EconomyOperation
{
    Set,
    Add,
    Take
}

public static class EconomyOperationExts
{
    public static void Apply(this EconomyOperation operation, ref long balance, long change)
    {
        switch (operation)
        {
            case EconomyOperation.Set: balance = change; break;
            case EconomyOperation.Add: balance += change; break;
            case EconomyOperation.Take: balance -= change; break;
        }
    }
}

public partial class Player
{
    public void ResetAllBalances(GameMode gameMode)
    {
        if (!HasPlayed(gameMode)) return;

        var playerGameMode = GameModes[gameMode.Id];
        playerGameMode.Balance = [];
    }
    
    private void ResetBalance(GameMode gameMode, string key)
    {
        if (!HasPlayed(gameMode)) return;

        var playerGameMode = GameModes[gameMode.Id];
        playerGameMode.Balance[key] = 0;
    }

    public void ChangeBalance(GameMode gameMode, string key, long change, EconomyOperation operation)
    {
        if (!HasPlayed(gameMode)) return;

        var playerGameMode = GameModes[gameMode.Id];

        if (!playerGameMode.Balance.ContainsKey(key))
            ResetBalance(gameMode, key);

        var balance = playerGameMode.Balance[key];
        operation.Apply(ref balance, change);
        
        playerGameMode.Balance[key] = balance;
    }

    public long GetBalance(GameMode gameMode, string key)
    {
        if (!HasPlayed(gameMode)) return 0;
        
        var playerGameMode = GameModes[gameMode.Id];

        if (!playerGameMode.Balance.ContainsKey(key))
            ResetBalance(gameMode, key);

        return playerGameMode.Balance[key];
    }
}