namespace EncounterVoteAPI.Models;

public class EncounterVoteDBSettings{
    public string ConnectionString{get;set;}=null!;
    public string DatabaseName{get;set;}=null!;
    public string EncountersCollectionName{get;set;}=null!;
    public string UsersCollectionName{get;set;}=null!;
    public string VotesCollectionName{get;set;}=null!;
    public string StrategiesCollectionName{get;set;}=null!;
}