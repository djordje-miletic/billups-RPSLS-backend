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
    public ActionResult<ChoicesResponse> Play(PlayRequest request)
    {
        return Ok(_choiceBL.Play(request.Player));
    }
}
