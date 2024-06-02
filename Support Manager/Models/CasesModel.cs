using Support_Manager.DataAccess;

namespace Support_Manager.Models
{
    public class CasesModel
    {
        public User User { get; set; }
        public List<Case> Cases { get; set; }
    }
}
