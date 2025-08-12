using Billups.RPLS.DAL.Interfaces;
using Billups.RPSLS.BL.Helper;
using Billups.RPSLS.BL.Interfaces;
using Billups.RPSLS.DataModel.Enums;
using Billups.RPSLS.DataModel.Responses;
using Billups.RPSLS.Entities;

namespace Billups.RPSLS.BL.Services;

public class ChoiceBL : IChoiceBL
{
    private readonly IChoiceDAL _choiceDAL;

    public ChoiceBL(IChoiceDAL choiceDAL)
    {
        _choiceDAL = choiceDAL;
    }

    public List<ChoicesResponse> GetAll()
    {
        List<ChoicesResponse> result = new List<ChoicesResponse>();

        List<ChoiceEntity> choiceEntities = _choiceDAL.GetAll();

        choiceEntities.ForEach(choiceEntity => 
        {
            result.Add(new ChoicesResponse(choiceEntity.Id, choiceEntity.Choice));
        });

        return result;
    }

    public ChoicesResponse Get()
    {
        Random rand = new Random();

        ChoiceEntity choiceEntity = _choiceDAL.GetById(rand.Next(0, 5));
    
        if(choiceEntity == null)
        {
            throw new Exception("Choice not found.");
        }

        return new ChoicesResponse(choiceEntity.Id, choiceEntity.Choice);
    }

    public PlayResponse Play(int player)
    {
        Random rand = new Random();

        int computer = rand.Next(1, 5);

        ResultEnum result = player.Play(computer);

        return new PlayResponse(result.ToString(), player, computer);
    }
}
