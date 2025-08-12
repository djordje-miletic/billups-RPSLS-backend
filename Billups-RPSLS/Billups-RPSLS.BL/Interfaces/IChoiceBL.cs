using Billups.RPSLS.DataModel.Responses;

namespace Billups.RPSLS.BL.Interfaces;

public interface IChoiceBL
{
    List<ChoicesResponse> GetAll();

    ChoicesResponse Get();

    PlayResponse Play(int player);
}
