using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace voteApi.Models;

public class Vote{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id {get;set;}

    [BsonElement("Name")]
    public string VoterId {get;set;} = null!;
    public string StratId {get;set;} = null!;
    public string EncounterId {get;set;} = null!;
    public string StratMedia{get;set;} = null!;
}