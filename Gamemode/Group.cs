namespace MinecraftServer;

public class Group
{
    public required string Id { get; init; }
    public HashSet<string> Permissions { get; init; } = [];
    
    public int Weight { get; init; }
    
    public string? Prefix { get; init; } = null;
    public string? Color { get; init; } = null;
    public string? Suffix { get; init; } = null;
}