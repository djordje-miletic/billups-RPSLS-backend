using Billups.RPSLS.BL.Helper;
using Billups.RPSLS.DAL.Interfaces;
using Billups.RPSLS.DataModel.Enums;
using Billups.RPSLS.DataModel.Requests;
using Billups.RPSLS.DataModel.Responses;
using Billups.RPSLS.DBContext;
using Billups.RPSLS.Entities;
using Microsoft.AspNetCore.SignalR;
using System.Collections.Concurrent;

namespace Billups.RPSL.Hubs;

public class GameHub : Hub
{
    private readonly IScoreDAL _scoreDAL;
    private readonly RPSLSDbContext _context;

    // Store waiting players
    private static ConcurrentQueue<string> waitingPlayers = new ConcurrentQueue<string>();

    // Track which group each player belongs to
    private static ConcurrentDictionary<string, string> playerGroups = new ConcurrentDictionary<string, string>();

    private static ConcurrentDictionary<string, ConcurrentDictionary<string, PlayRequest>> gameMoves = new ConcurrentDictionary<string, ConcurrentDictionary<string, PlayRequest>>();

    public GameHub(IScoreDAL scoreDAL,
        RPSLSDbContext context)
    {
        _scoreDAL = scoreDAL;
        _context = context;
    }

    public override async Task OnConnectedAsync()
    {
        if (!waitingPlayers.TryDequeue(out var waitingPlayerId))
        {
            waitingPlayers.Enqueue(Context.ConnectionId);
            await Clients.Caller.SendAsync("WaitingForOpponent");
        }
        else
        {
            var groupName = $"Game_{Guid.NewGuid()}";

            playerGroups[Context.ConnectionId] = groupName;
            playerGroups[waitingPlayerId] = groupName;

            await Groups.AddToGroupAsync(Context.ConnectionId, groupName);
            await Groups.AddToGroupAsync(waitingPlayerId, groupName);

            gameMoves[groupName] = new ConcurrentDictionary<string, PlayRequest>();

            await Clients.Group(groupName).SendAsync("GameStarted", "Two players connected! Let’s play!");
        }

        await base.OnConnectedAsync();
    }

    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        ConcurrentQueue<string> updatedQueue = new ConcurrentQueue<string>();
        while (waitingPlayers.TryDequeue(out var id))
        {
            if (id != Context.ConnectionId)
            {
                updatedQueue.Enqueue(id);
            }
        }
        waitingPlayers = updatedQueue;

        if (playerGroups.TryRemove(Context.ConnectionId, out var groupName))
        {
            if (gameMoves.ContainsKey(groupName))
            {
                gameMoves[groupName].TryRemove(Context.ConnectionId, out _);

                var opponentId = playerGroups
                    .Where(kvp => kvp.Value == groupName)
                    .Select(kvp => kvp.Key)
                    .FirstOrDefault();

                if (opponentId != null)
                {
                    await Clients.Client(opponentId).SendAsync("OpponentDisconnected");
                    playerGroups.TryRemove(opponentId, out _);
                    gameMoves[groupName].TryRemove(opponentId, out _);
                }

                gameMoves.TryRemove(groupName, out _);
            }
        }

        await base.OnDisconnectedAsync(exception);
    }

    public async Task SendMove(PlayRequest move)
    {
        if (!playerGroups.TryGetValue(Context.ConnectionId, out var groupName))
        {
            return;
        }

        ConcurrentDictionary<string, PlayRequest> moves = gameMoves[groupName];
        moves[Context.ConnectionId] = new PlayRequest(move.Player, move.PlayerName);

        if (moves.Count == 2)
        {
            List<string> players = moves.Keys.ToList();
            string p1 = players[0];
            string p2 = players[1];

            ResultEnum resultPlayer1 = moves[p1].Player.Play(moves[p2].Player);
            ResultEnum resultPlayer2 = Player2Result(resultPlayer1);

            await Clients.Client(p1).SendAsync("RoundResult", new RoundHistory(moves[p1].Player, moves[p2].Player, ReturnMessage(resultPlayer1)));
            await Clients.Client(p2).SendAsync("RoundResult", new RoundHistory(moves[p2].Player, moves[p1].Player, ReturnMessage(resultPlayer2)));

            ScoreEntity scoreEntityPlayer1 = new ScoreEntity(resultPlayer1, moves[p1].PlayerName);
            ScoreEntity scoreEntityPlayer2 = new ScoreEntity(resultPlayer2, moves[p2].PlayerName);

            _scoreDAL.Insert(scoreEntityPlayer1);
            _scoreDAL.Insert(scoreEntityPlayer2);

            _context.SaveChanges();

            moves.Clear();
        }
        else
        {
            await Clients.Caller.SendAsync("WaitingForOpponentMove");
        }
    }

    private string ReturnMessage(ResultEnum result)
    {
        switch (result)
        {
            case ResultEnum.Win:
                return "You have won!";
            case ResultEnum.Lose:
                return "You lost!";
            case ResultEnum.Tie:
                return "It's a tie";
            default:
                return "";
        }
    }

    private ResultEnum Player2Result(ResultEnum resultEnum)
    {
        switch(resultEnum)
        {
            case ResultEnum.Win:
                return ResultEnum.Lose;
            case ResultEnum.Lose:
                return ResultEnum.Win;
            case ResultEnum.Tie:
                return ResultEnum.Tie;
        }

        return ResultEnum.Tie;
    }
}
