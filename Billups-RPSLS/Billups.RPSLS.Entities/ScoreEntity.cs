using Billups.RPSLS.DataModel.Enums;

namespace Billups.RPSLS.Entities
{
    public class ScoreEntity
    {
        public ScoreEntity(int id,
            ResultEnum result,
            string playerName)
        {
            Id = id;
            Result = result;
            PlayerName = playerName;
            CreatedOn = DateTime.Now;
        }

        public ScoreEntity(ResultEnum result,
            string playerName)
        {
            Result = result;
            PlayerName = playerName;
            CreatedOn = DateTime.Now;
        }

        public int Id { get; set; }

        public ResultEnum Result { get; set; }

        public string PlayerName { get; set; } = string.Empty;

        public DateTime CreatedOn { get; set; }

        public bool IsDeleted { get; set; } = false; 
    }
}
