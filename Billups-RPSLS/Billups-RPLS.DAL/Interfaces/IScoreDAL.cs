using Billups.RPSLS.Entities;

namespace Billups.RPSLS.DAL.Interfaces;

public interface IScoreDAL : IBaseDAL<ScoreEntity>
{
    Task<List<ScoreEntity>> Get10RecentResults();

    Task<List<ScoreEntity>> Get10RecentResultsByPlayer(string name);

    Task<List<ScoreEntity>> GetByPlayer(string name);
}
