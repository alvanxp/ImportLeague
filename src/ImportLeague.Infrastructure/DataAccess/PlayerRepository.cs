using ImportLeague.Core.Entities;
using ImportLeague.Core.Interfaces.Repository;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ImportLeague.Infrastructure.DataAccess
{
    public class PlayerRepository : EntityRepository<Player>, IPlayerRepository
    {
        public PlayerRepository(LeagueContext dbContext) : base(dbContext)
        {

        }
        public async Task<int> GetNroPlayersByCompetition(string leagueCode)
        {
            var counter = await (from p in Context.Player
                                 join t in Context.Team on p.TeamId equals t.Id
                                 join tc in Context.TeamByCompetition on t.Id equals tc.TeamId
                                 join c in Context.Competition on tc.CompetitionId equals c.Id
                                 where c.Code == leagueCode
                                 select 1).CountAsync();
            return counter;
        }
    }
}
