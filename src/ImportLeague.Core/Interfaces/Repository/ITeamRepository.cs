using ImportLeague.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ImportLeague.Core.Interfaces.Repository
{
    public interface ITeamRepository : IRepository<Team>
    {
        Task<HashSet<long>> GetNotImportedTeams(IEnumerable<Team> teams);
    }
}
