namespace MinecraftServer;

public class Group
{
    public string Id { get; set; } = null!;
    public HashSet<string> Permissions { get; set; } = [];
    public int Weight { get; set; } = 0;
    
    public string Prefix { get; set; } = null!;
    public string Color { get; set; } = null!;
    public string Suffix { get; set; } = null!;
}