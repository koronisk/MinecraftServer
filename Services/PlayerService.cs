using MinecraftServer.Models;
using MongoDB.Driver;

namespace MinecraftServer.Services;

public class PlayerService
{
    private readonly IMongoCollection<Player> _players;

    public PlayerService(IConfiguration config)
    {
        var client = new MongoClient(config.GetConnectionString("MongoDb"));
        var database = client.GetDatabase("test");

        _players = database.GetCollection<Player>("Players");
    }

    public async Task<List<Player>> GetAllAsync() =>
        await _players.Find(_ => true).ToListAsync();

    public async Task<Player?> GetByNameAsync(string name) =>
        await _players.Find(u => u.Name == name).FirstOrDefaultAsync();

    public async Task CreateAsync(Player player) =>
        await _players.InsertOneAsync(player);
}