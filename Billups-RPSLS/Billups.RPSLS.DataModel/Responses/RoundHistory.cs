namespace Billups.RPSLS.DataModel.Responses;

public class RoundHistory
{
    public RoundHistory(int playerChoice,
        int opponentChoice,
        string result)
    {
        PlayerChoice = playerChoice;
        OpponentChoice = opponentChoice;
        Result = result;
    }

    public int PlayerChoice { get; private set; }

    public int OpponentChoice { get; private set; }

    public string Result { get; private set; } = string.Empty;
}
