namespace Billups.RPSLS.Entities;

public class ResultEntity
{
    public ResultEntity(int id,
        string result)
    {
        Id = id;
        Result = result;
    }

    public int Id { get; set; }
    public string Result { get; set; } = string.Empty;
}
