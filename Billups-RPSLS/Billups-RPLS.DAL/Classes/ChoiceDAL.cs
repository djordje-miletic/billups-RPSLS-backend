using Billups.RPLS.DAL.Classes;
using Billups.RPSLS.DAL.Interfaces;
using Billups.RPSLS.DBContext;
using Billups.RPSLS.Entities;

namespace Billups.RPSLS.DAL.Classes;

public class ChoiceDAL : BaseDAL<ChoiceEntity>, IChoiceDAL
{
    public ChoiceDAL(RPSLSDbContext context) 
        : base(context)
    {
        
    }

    //public ChoiceEntity? Get(int id)
    //{
    //    return _context.Choices.Where(c => c.Id == id).FirstOrDefault();
    //}
}
