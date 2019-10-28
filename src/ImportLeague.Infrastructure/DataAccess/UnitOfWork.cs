using ImportLeague.Core.Entities;
using ImportLeague.Core.Interfaces.Repository;
using System;
using System.Threading.Tasks;

namespace ImportLeague.Infrastructure.DataAccess
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly LeagueContext _dbContext;

        public UnitOfWork(LeagueContext dbContext)
        {
            _dbContext = dbContext;
            Competitions = new EntityRepository<Competition>(dbContext);
            Teams = new TeamRepository(dbContext);
            Players = new PlayerRepository(dbContext);
            TeamByCompetition = new EntityRepository<TeamByCompetition>(dbContext);

        }
        public IRepository<Competition> Competitions { get; set; }

        public ITeamRepository Teams { get; }

        public IPlayerRepository Players { get; }

        public IRepository<TeamByCompetition> TeamByCompetition { get; }

        public IDatabaseTransaction BeginTrainsaction()
        {
            return new EntityDatabaseTransaction(_dbContext);
        }

        public async Task<int> Save()
        {
            return await _dbContext.SaveChangesAsync();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _dbContext?.Dispose();
            }
        }
    }
}
