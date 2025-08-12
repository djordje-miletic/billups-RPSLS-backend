namespace Billups.RPSLS.Entities;

public class ChoiceEntity
{
    public ChoiceEntity(int id,
        string choice)
    {
        Id = id;
        Choice = choice;
    }

    public int Id { get; set; }

    public string Choice { get; set; } = string.Empty;
}
