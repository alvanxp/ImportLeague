using ImportLeague.Core.Entities;
using ImportLeague.Core.Interfaces.Repository;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ImportLeague.Infrastructure.DataAccess
{
    public class TeamRepository : EntityRepository<Team>, ITeamRepository
    {
        public TeamRepository(LeagueContext dbContext) : base(dbContext)
        {

        }
        public async Task<HashSet<long>> GetNotImportedTeams(IEnumerable<Team> teams)
        {
            var teamIds = new HashSet<long>(await this.Context.Team.Select(_ => _.Id).ToListAsync());
            return new HashSet<long>(teams.Where(t => !teamIds.Contains(t.Id)).Select(_ => _.Id));
        }
    }
}
