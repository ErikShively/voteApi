using EncounterVoteAPI.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace EncounterVoteAPI.Services;
// Probably just need the one thing here
public class APIService{
    private readonly IMongoCollection<Encounter> _encountersCollection;
    private readonly IMongoCollection<User> _usersCollection;
    private readonly IMongoCollection<Vote> _votesCollection;
    private readonly IMongoCollection<Strategy> _strategiesCollection;

    public APIService(IOptions<EncounterVoteDBSettings> APIDBSettings){
        var mongoClient = new MongoClient(APIDBSettings.Value.ConnectionString);
        var mongoDatabase = mongoClient.GetDatabase(APIDBSettings.Value.DatabaseName);
        _encountersCollection = mongoDatabase.GetCollection<Encounter>(APIDBSettings.Value.EncountersCollectionName);
        _usersCollection = mongoDatabase.GetCollection<User>(APIDBSettings.Value.UsersCollectionName);
        _votesCollection = mongoDatabase.GetCollection<Vote>(APIDBSettings.Value.VotesCollectionName);
        _strategiesCollection = mongoDatabase.GetCollection<Strategy>(APIDBSettings.Value.StrategiesCollectionName);
    }
    public async Task<List<Encounter>> GetEncountersAsync()=>
        await _encountersCollection.Find(_=>true).ToListAsync();
    public async Task<Encounter?> GetEncountersAsync(string id)=>
        await _encountersCollection.Find(x=>x.Id==id).FirstOrDefaultAsync();
    public async Task CreateEncounterAsync(Encounter newEncounter)=>
        await _encountersCollection.InsertOneAsync(newEncounter);
    public async Task UpdateEncounterAsync(string id, Encounter updatedEncounter)=>
        await _encountersCollection.ReplaceOneAsync(x=>x.Id==id, updatedEncounter);
    public async Task RemoveEncounterAsync(string id)=>
        await _encountersCollection.DeleteOneAsync(x=>x.Id==id);

    public async Task<List<User>> GetUsersAsync()=>
        await _usersCollection.Find(_=>true).ToListAsync();
    public async Task<User?> GetUsersAsync(string id)=>
        await _usersCollection.Find(x=>x.Id==id).FirstOrDefaultAsync();
    
    public async Task CreateUserAsync(User newUser)=>
        await _usersCollection.InsertOneAsync(newUser);
    public async Task UpdateUserAsync(string id, User updatedUser)=>
        await _usersCollection.ReplaceOneAsync(x=>x.Id==id, updatedUser);
    public async Task RemoveUserAsync(string id)=>
        await _usersCollection.DeleteOneAsync(x=>x.Id==id);
    
    public async Task<List<Vote>> GetVotesAsync()=>
        await _votesCollection.Find(_=>true).ToListAsync();
    public async Task<Vote?> GetVotesAsync(string id)=>
        await _votesCollection.Find(x=>x.Id==id).FirstOrDefaultAsync();
    public async Task<Vote?> GetVotesByVoterIDAsync(string id)=>
        await _votesCollection.Find(x=>x.VoterId==id).FirstOrDefaultAsync();
    public async Task<long> GetStrategyVoteCountAsync(string id)=>
        await _votesCollection.CountDocumentsAsync(x=>x.StrategyId==id);
    public async Task CreateVoteAsync(Vote newVote)=>
        await _votesCollection.InsertOneAsync(newVote);
    public async Task UpdateVoteAsync(string id, Vote updatedVote)=>
        await _votesCollection.ReplaceOneAsync(x=>x.Id==id, updatedVote);
    public async Task RemoveVoteAsync(string id)=>
        await _votesCollection.DeleteOneAsync(x=>x.Id==id);
    
    public async Task<List<Strategy>> GetStrategiesAsync()=>
        await _strategiesCollection.Find(_=>true).ToListAsync();
    public async Task<Strategy?> GetStrategiesAsync(string id)=>
        await _strategiesCollection.Find(x=>x.Id==id).FirstOrDefaultAsync();
    public async Task<Strategy?> GetStrategyByVoteAsync(Vote vote)=>
        await _strategiesCollection.Find(x=>x.Id==vote.StrategyId).FirstOrDefaultAsync();
    public async Task CreateStrategyAsync(Strategy newStrategy)=>
        await _strategiesCollection.InsertOneAsync(newStrategy);
    public async Task UpdateStrategyAsync(string id, Strategy updatedStrategy)=>
        await _strategiesCollection.ReplaceOneAsync(x=>x.Id==id, updatedStrategy);
    public async Task RemoveStrategyAsync(string id)=>
        await _strategiesCollection.DeleteOneAsync(x=>x.Id==id);
}