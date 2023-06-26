using voteApi.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace voteApi.Services;

public class VotesService{
    private readonly IMongoCollection<Vote> _votesCollection;

    public VotesService(IOptions<VoteDBSettings> voteDBSettings){
        var mongoClient = new MongoClient(voteDBSettings.Value.ConnectionString);
        var mongoDatabase = mongoClient.GetDatabase(voteDBSettings.Value.DatabaseName);
        _votesCollection = mongoDatabase.GetCollection<Vote>(voteDBSettings.Value.VotesCollectionName);
    }
    public async Task<List<Vote>> GetAsync()=>
        await _votesCollection.Find(_=>true).ToListAsync();
    public async Task<Vote?> GetAsync(string id)=>
        await _votesCollection.Find(x=>x.Id == id).FirstOrDefaultAsync();
    public async Task<List<Vote>> GetStratVotes(string id)=>
        await _votesCollection.Find(x=>x.StratId == id).ToListAsync();
    // Return a list of votes for a specific strat
    public async Task CreateAsync(Vote newVote) =>
        await _votesCollection.InsertOneAsync(newVote);
    public async Task UpdateAsync(string id, Vote updatedVote) =>
        await _votesCollection.ReplaceOneAsync(x => x.Id == id, updatedVote);
    public async Task RemoveAsync(string id) =>
        await _votesCollection.DeleteOneAsync(x => x.Id == id);
}