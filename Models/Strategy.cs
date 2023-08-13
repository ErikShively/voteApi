using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace EncounterVoteAPI.Models;

public class Strategy{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id{get;set;}

    public string Name{get;set;} = null!;
    public string StrategyMedia{get;set;} = null!;
    public string EncounterId{get;set;} = null!;
    public int Votes{get;set;} = 0;
    // If possible run a find of all votes tied to this encounter here to get an accurate count at startup
}