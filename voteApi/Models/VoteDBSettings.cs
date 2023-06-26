namespace voteApi.Models;
public class VoteDBSettings
{
    public string ConnectionString { get; set; } = null!;

    public string DatabaseName { get; set; } = null!;

    public string VotesCollectionName { get; set; } = null!;
}