namespace Billups.RPSLS.DataModel.Requests;

public class PlayRequest
{
    public PlayRequest(int player,
        string playerName)
    {
        Player = player;
        PlayerName = playerName;
    }

    public int Player { get; set; }

    public string PlayerName { get; set; } = string.Empty;
}
