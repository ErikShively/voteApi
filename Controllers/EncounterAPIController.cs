using EncounterVoteAPI.Models;
using EncounterVoteAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace EncounterVoteAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DataController:ControllerBase
{
    private readonly APIService _apiService;
    public DataController(APIService apiService)=>
        _apiService = apiService;
    [HttpGet]
    public async Task<List<Strategy>> Get()=>
    await _apiService.GetStrategiesAsync();
    [HttpGet("vote")]
    public async Task<List<Vote>> GetVotes()=>
    await _apiService.GetVotesAsync();
    [HttpGet("vote/{id:length(24)}")]
    public async Task<ActionResult<Vote>> GetVotes(string id)
    {
        var vote = await _apiService.GetVotesAsync(id);
        if(vote is null)
        {
            return NotFound();
        }
        return vote;
    }
    [HttpPost("vote/{id:length(24)}")]
    public async Task<IActionResult> PostVote(Vote newVote)
    {
        var priorVote = await _apiService.GetVotesByVoterIDAsync(newVote.VoterId);
        if(priorVote is null){ //Else, update
            var affectedStrategy = await _apiService.GetStrategyByVoteAsync(newVote);
            if(affectedStrategy is not null && affectedStrategy.Id is not null)
            {
                // Might need better null checking here
                affectedStrategy.Votes = Convert.ToInt32(await _apiService.GetStrategyVoteCountAsync(affectedStrategy.Id));
                await _apiService.UpdateStrategyAsync(affectedStrategy.Id, affectedStrategy);
                await _apiService.CreateVoteAsync(newVote);
                return CreatedAtAction(nameof(GetVotes), new {id=newVote.Id}, newVote);
            }
        }
        // Return an error about a duplicate/prior vote
        return NotFound();
        // Update the strat count, probably check if the user already has an existing vote
    }

    [HttpGet("strategy")]
    public async Task<List<Strategy>> GetStrategies() =>
        await _apiService.GetStrategiesAsync();
    [HttpGet("strategy/{id:length(24)}")]
    public async Task<ActionResult<Strategy>> GetStrategies(string id)
    {
        var strategy = await _apiService.GetStrategiesAsync(id);
        if(strategy is null)
        {
            return NotFound();
        }
        return strategy;
    }
    [HttpPost("strategy")]
    public async Task<IActionResult> PostEncounter(Strategy newStrategy){
        await _apiService.CreateStrategyAsync(newStrategy);
        return CreatedAtAction(nameof(GetStrategies), new {id=newStrategy.Id}, newStrategy);
    }
    [HttpPut("strategy/{id:length(24)}")]
    public async Task<IActionResult> Update(string id, Strategy updatedStrategy)
    {
        var strategy = await _apiService.GetStrategiesAsync(id);

        if (strategy is null)
        {
            return NotFound();
        }

        updatedStrategy.Id = strategy.Id;

        await _apiService.UpdateStrategyAsync(id, updatedStrategy);

        return NoContent();
    }
    [HttpDelete("strategy/{id:length(24)}")]
    public async Task<IActionResult> Delete(string id)
    {
        var strategy = await _apiService.GetStrategiesAsync(id);

        if (strategy is null)
        {
            return NotFound();
        }

        await _apiService.RemoveStrategyAsync(id);

        return NoContent();
    }
}