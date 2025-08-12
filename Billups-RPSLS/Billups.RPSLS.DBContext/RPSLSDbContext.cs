using Billups.RPSLS.Entities;
using Microsoft.EntityFrameworkCore;

namespace Billups.RPSLS.DBContext;

public class RPSLSDbContext : DbContext
{
    public DbSet<ChoiceEntity> Choices {  get; set; } 

    public DbSet<ResultEntity> Results { get; set; }

    public DbSet<ScoreEntity> Scores { get; set; }

    public RPSLSDbContext(DbContextOptions options) 
        : base(options)
    {
        
    }
}
