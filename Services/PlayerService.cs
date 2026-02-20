using MinecraftServer.Models;
using MongoDB.Driver;

namespace MinecraftServer.Services;

public class PlayerService
{
    private readonly IMongoCollection<Models.Player> _players;

    public PlayerService(IConfiguration config)
    {
        var client = new MongoClient(config.GetConnectionString("MongoDb"));
        var database = client.GetDatabase("test");

        _players = database.GetCollection<Models.Player>("Players");
    }

    public async Task<List<Models.Player>> GetAllAsync() =>
        await _players.Find(_ => true).ToListAsync();

    public async Task<Models.Player?> GetByNameAsync(string name) =>
        await _players.Find(u => u.Name == name).FirstOrDefaultAsync();

    public async Task CreateAsync(Models.Player player) =>
        await _players.InsertOneAsync(player);
}