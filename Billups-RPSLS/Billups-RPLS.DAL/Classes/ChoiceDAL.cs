using Billups.RPLS.DAL.Classes;
using Billups.RPSLS.DBContext;
using Billups.RPSLS.Entities;
using Billups.RPLS.DAL.Interfaces;

namespace Billups.RPLS.DAL.Classes;

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
