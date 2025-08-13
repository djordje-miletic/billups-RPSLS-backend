using Billups.RPSLS.BL.Interfaces;
using Billups.RPSLS.DataModel.Requests;
using Billups.RPSLS.DataModel.Responses;
using Microsoft.AspNetCore.Mvc;

namespace Billups_RPSLS.Controllers;

public class RPSLSController : Controller
{
    private readonly IChoiceBL _choiceBL;

    public RPSLSController(IChoiceBL choiceBL)
    {
        _choiceBL = choiceBL;
    }

    [HttpGet("/choices")]
    public ActionResult<List<ChoicesResponse>> Choices()
    {
        return Ok(_choiceBL.GetAll());
    }

    [HttpGet("/choice")]
    public ActionResult<ChoicesResponse> Choice()
    {
        return Ok(_choiceBL.Get());
    }

    [HttpPost("/play")]
    public ActionResult<ChoicesResponse> Play([FromBody] PlayRequest request)
    {
        return Ok(_choiceBL.Play(request));
    }

    [HttpGet("get10RecentResults")]
    public async Task<ActionResult<List<ScoreResponse>>> Get10RecentResults()
    {
        return Ok(await _choiceBL.Get10RecentResults());
    }

    [HttpGet("get10RecentResultsByPlayer/{playerName}")]
    public async Task<ActionResult<List<ScoreResponse>>> Get10RecentResults(string playerName)
    {
        return Ok(await _choiceBL.Get10RecentResultsByPlayer(playerName));
    }

    [HttpDelete("reset")]
    public ActionResult Reset()
    {
        _choiceBL.Reset();
        return Ok();
    }

    [HttpDelete("resetByPlayer/{playerName}")]
    public async Task<ActionResult> ResetByPlayer(string playerName)
    {
        await _choiceBL.ResetByPlayer(playerName);
        return Ok();
    }
}
