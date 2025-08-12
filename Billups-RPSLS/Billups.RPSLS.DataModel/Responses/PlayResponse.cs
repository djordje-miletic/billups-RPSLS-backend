namespace Billups.RPSLS.DataModel.Responses;

public class PlayResponse
{
    public PlayResponse(string result,
        int player,
        int computer)
    {
        Results = result;
        Player = player;
        Computer = computer;
    }

    public string Results { get; set; } = string.Empty;

    public int Player { get; set; }

    public int Computer { get; set;}
}
