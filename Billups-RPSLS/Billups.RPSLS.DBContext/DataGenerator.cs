using Billups.RPSLS.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Billups.RPSLS.DBContext;

public class DataGenerator
{
    public static void Initialize(IServiceProvider serviceProvider)
    {
        using (var context = new RPSLSDbContext(
            serviceProvider.GetRequiredService<DbContextOptions<RPSLSDbContext>>()))
        {
            AddChoices(context);
            AddResults(context);

            context.SaveChanges();
        }
    }

    private static void AddChoices(RPSLSDbContext context)
    {
        context.Add(new ChoiceEntity(1, "Rock"));
        context.Add(new ChoiceEntity(2, "Paper"));
        context.Add(new ChoiceEntity(3, "Scissors"));
        context.Add(new ChoiceEntity(4, "Lizard"));
        context.Add(new ChoiceEntity(5, "Spock"));
    }

    private static void AddResults(RPSLSDbContext context)
    {
        context.Add(new ResultEntity(1, "Win"));
        context.Add(new ResultEntity(2, "Lose"));
        context.Add(new ResultEntity(3, "Tie"));
    }
}
