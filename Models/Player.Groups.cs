namespace MinecraftServer.Models;

public partial class Player
{
    public void ResetGroups() => Groups.Clear();
    public void ResetGroups(GameMode gameMode)
    {
        if (!HasPlayed(gameMode)) return;

        var playerGameMode = GameModes[gameMode.Id];
        playerGameMode.Groups.Clear();
    }

    public void AddGroup(Group group) => Groups.Add(group);
    public void AddGroup(GameMode gameMode, Group group)
    {
        if (!HasPlayed(gameMode)) return;

        var playerGameMode = GameModes[gameMode.Id];
        playerGameMode.Groups.Add(group);
    }

    public void RemoveGroup(Group group) => Groups.Remove(group);
    public void RemovesGroup(GameMode gameMode, Group group)
    {
        if (!HasPlayed(gameMode)) return;

        var playerGameMode = GameModes[gameMode.Id];
        playerGameMode.Groups.Remove(group);
    }

    public List<Group> GetGroups() => Groups;
    public List<Group> GetGroups(GameMode gameMode)
    {
        if (!HasPlayed(gameMode)) return [];

        var playerGameMode = GameModes[gameMode.Id];
        return playerGameMode.Groups;
    }

    public Group? GetHighestGroup() => Groups.Count != 0 ? Groups.MaxBy(x => x.Weight) : null;
    public Group? GetHighestGroup(GameMode gameMode)
    {
        if (!HasPlayed(gameMode)) return null;

        var playerGameMode = GameModes[gameMode.Id];
        return playerGameMode.Groups.Count != 0 ? playerGameMode.Groups.MaxBy(x => x.Weight) : null;
    }
}