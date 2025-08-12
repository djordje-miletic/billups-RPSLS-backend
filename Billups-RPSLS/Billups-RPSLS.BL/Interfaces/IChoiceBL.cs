using Billups.RPSLS.DataModel.Requests;
using Billups.RPSLS.DataModel.Responses;

namespace Billups.RPSLS.BL.Interfaces;

public interface IChoiceBL
{
    List<ChoicesResponse> GetAll();

    ChoicesResponse Get();

    PlayResponse Play(PlayRequest player);

    Task<List<ScoreResponse>> Get10RecentResults();

    Task<List<ScoreResponse>> Get10RecentResultsByPlayer(string playerName);

    void Reset();

    Task ResetByPlayer(string playerName);
}
