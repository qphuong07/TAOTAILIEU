using DAL;

namespace BLL
{
    public class BaseBLL
    {
        protected TAOTAILIEUEntities Db { get; } = new TAOTAILIEUEntities();
    }
}
