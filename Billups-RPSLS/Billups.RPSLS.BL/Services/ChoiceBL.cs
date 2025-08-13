using Billups.RPSLS.BL.Helper;
using Billups.RPSLS.BL.Interfaces;
using Billups.RPSLS.DAL.Interfaces;
using Billups.RPSLS.DataModel.Enums;
using Billups.RPSLS.DataModel.Requests;
using Billups.RPSLS.DataModel.Responses;
using Billups.RPSLS.DBContext;
using Billups.RPSLS.Entities;

namespace Billups.RPSLS.BL.Services;

public class ChoiceBL : IChoiceBL
{
    private readonly RPSLSDbContext _context;
    private readonly IChoiceDAL _choiceDAL;
    private readonly IScoreDAL _scoreDAL;

    public ChoiceBL(RPSLSDbContext context,
        IChoiceDAL choiceDAL, 
        IScoreDAL scoreDAL)
    {
        _context = context;
        _choiceDAL = choiceDAL;
        _scoreDAL = scoreDAL;
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

    public PlayResponse Play(PlayRequest player)
    {
        Random rand = new Random();

        int computer = rand.Next(1, 5);

        ResultEnum result = player.Player.Play(computer);

        ScoreEntity scoreEntity = new ScoreEntity(result, player.PlayerName);

        _scoreDAL.Insert(scoreEntity);
        _context.SaveChanges();

        return new PlayResponse(result.ToString(), player.Player, computer);
    }

    public async Task<List<ScoreResponse>> Get10RecentResults()
    {
        List<ScoreResponse> result = new List<ScoreResponse>();
        List<ScoreEntity> scoreEntities = await _scoreDAL.Get10RecentResults();

        if(scoreEntities is not null && scoreEntities.Count > 0)
        {
            scoreEntities.ForEach(s =>
            {
                result.Add(new ScoreResponse(s.PlayerName, s.Result.ToString()));
            });

            return result;
        }

        throw new Exception("No new scores found!");
    }

    public async Task<List<ScoreResponse>> Get10RecentResultsByPlayer(string playerName)
    {
        List<ScoreResponse> result = new List<ScoreResponse>();
        List<ScoreEntity> scoreEntities = await _scoreDAL.Get10RecentResultsByPlayer(playerName);

        if (scoreEntities is not null && scoreEntities.Count > 0)
        {
            scoreEntities.ForEach(s =>
            {
                result.Add(new ScoreResponse(s.PlayerName, s.Result.ToString()));
            });

            return result;
        }

        throw new Exception("No scores found!");
    }

    public void Reset()
    {
        List<ScoreEntity> scoreEntities = _scoreDAL.GetAll();

        scoreEntities.ForEach(s =>
        {
            s.IsDeleted = true;
            _scoreDAL.Update(s);
        });

        _context.SaveChanges();
    }

    public async Task ResetByPlayer(string playerName)
    {
        List<ScoreEntity> scoreEntities = await _scoreDAL.GetByPlayer(playerName);

        scoreEntities.ForEach(s =>
        {
            s.IsDeleted = true;
            _scoreDAL.Update(s);
        });

        _context.SaveChanges();
    }
}
