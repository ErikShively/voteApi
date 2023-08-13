using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace EncounterVoteAPI.Models;

public class Encounter{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id{get;set;}

    public string Name{get;set;} = null!;
    public string Difficulty{get;set;} = null!;
    public string Patch{get;set;} = null!;
}