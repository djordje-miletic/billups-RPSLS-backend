namespace Billups.RPSLS.DataModel.Responses;

public class ChoicesResponse
{
    public ChoicesResponse(int id,
        string name)
    {
        Id = id;
        Name = name;
    }

    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;
}
