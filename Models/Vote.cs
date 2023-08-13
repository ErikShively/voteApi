using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace EncounterVoteAPI.Models;

public class Vote{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id{get;set;}
    public string VoterId{get;set;} = null!;
    public string StrategyId{get;set;} = null!;
}