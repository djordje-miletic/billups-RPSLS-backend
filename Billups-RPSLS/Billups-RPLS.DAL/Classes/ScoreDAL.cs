using Billups.RPLS.DAL.Classes;
using Billups.RPSLS.DAL.Interfaces;
using Billups.RPSLS.DBContext;
using Billups.RPSLS.Entities;
using Microsoft.EntityFrameworkCore;

namespace Billups.RPSLS.DAL.Classes;

public class ScoreDAL : BaseDAL<ScoreEntity>, IScoreDAL
{
    public ScoreDAL(RPSLSDbContext context) 
        : base(context)
    {
    }

    public async Task<List<ScoreEntity>> Get10RecentResults()
    {
        return await _context.Scores.Where(s => s.IsDeleted == false).OrderByDescending(s => s.CreatedOn).ToListAsync();
    }

    public async Task<List<ScoreEntity>> Get10RecentResultsByPlayer(string name)
    {
        return await _context.Scores.Where(s => s.PlayerName == name && s.IsDeleted == false).OrderByDescending(s => s.CreatedOn).ToListAsync();
    }

    public async Task<List<ScoreEntity>> GetByPlayer(string name)
    {
        return await _context.Scores.Where(s => s.PlayerName == name && s.IsDeleted == false).ToListAsync();
    }
}
