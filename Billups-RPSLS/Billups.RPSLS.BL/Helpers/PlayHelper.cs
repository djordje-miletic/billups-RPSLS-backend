using Billups.RPSLS.DataModel.Enums;
using Billups.RPSLS.Entities;

namespace Billups.RPSLS.BL.Helper;

public static class PlayHelper
{
    public static ResultEnum Play(this int player, int computer)
    {
        if (player == computer)
        {
            return ResultEnum.Tie;
        }

        var winsAgainst = new (int choice, int[] beats)[]
        {
            (1, new[] { 3, 4 }), // Rock beats Scissors, Lizard
            (2, new[] { 1, 5 }), // Paper beats Rock, Spock
            (3, new[] { 2, 4 }), // Scissors beats Paper, Lizard
            (4, new[] { 2, 5 }), // Lizard beats Paper, Spock
            (5, new[] { 1, 3 })  // Spock beats Rock, Scissors
        };

        foreach (var rule in winsAgainst)
        {
            if (rule.choice == player && Array.Exists(rule.beats, x => x == computer))
            {
                return ResultEnum.Win;
            }
            if (rule.choice == computer && Array.Exists(rule.beats, x => x == player))
            {
                return ResultEnum.Lose;
            }
        }

        throw new Exception("Result can't be found!");
    }
}
