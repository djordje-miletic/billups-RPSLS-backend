using Billups.RPSLS.DataModel.Enums;

namespace Billups.RPSLS.DataModel.Responses;

public class ScoreResponse
{
    public ScoreResponse(string playerName,
        string result)
    {
        PlayerName = playerName;
        Result = result;
    }

    public string PlayerName { get; private set; } = string.Empty;
    public string Result { get; private set; }
}
