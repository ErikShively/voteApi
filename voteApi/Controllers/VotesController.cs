using voteApi.Models;
using voteApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace voteApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class VotesController : ControllerBase{
    private readonly VotesService _votesService;

    public VotesController(VotesService votesService) =>
        _votesService = votesService;

    [HttpGet]
    public async Task<List<Vote>> Get() =>
        await _votesService.GetAsync();

    [HttpGet("{id:length(24)}")]
    public async Task<ActionResult<Vote>> Get(string id)
    {
        var vote = await _votesService.GetAsync(id);

        if(vote is null)
        {
            return NotFound();
        }
        
        return vote;
    }
    [HttpGet("{stratid:length(24)}")]
    public async Task<List<Vote>> GetStratVotes(string id){
        List<Vote> votes = await _votesService.GetStratVotes(id);
        return votes;
    }
    [HttpPost]
    public async Task<IActionResult> Post(Vote newVote){
        await _votesService.CreateAsync(newVote);
        return CreatedAtAction(nameof(Get), new{id=newVote.Id},newVote);
        // Might need to tally the votes here, but concerned about continuity. Will likely need to make a new entity for strats.
        // Counting the votes at get would probably work, but it would be very inefficient.
    }
    [HttpPut("id:length(24)}")]
    public async Task<IActionResult> Update(string id, Vote updatedVote)
    {
        var vote = await _votesService.GetAsync(id);
        if(vote is null){
            return NotFound();
        }
        updatedVote.Id = vote.Id;
        await _votesService.UpdateAsync(id,updatedVote);
        return NoContent();
    }
    [HttpDelete("{id:length(24)}")]
    public async Task<IActionResult> Delete(string id)
    {
        var vote = await _votesService.GetAsync(id);
        if (vote is null)
        {
            return NotFound();
        }
        await _votesService.RemoveAsync(id);
        return NoContent();
    }
}