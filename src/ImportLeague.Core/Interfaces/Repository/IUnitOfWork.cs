using ImportLeague.Core.Entities;
using System;
using System.Threading.Tasks;

namespace ImportLeague.Core.Interfaces.Repository
{
    public interface IUnitOfWork : IDisposable
    {
        IDatabaseTransaction BeginTrainsaction();
        IRepository<Competition> Competitions { get; }

        ITeamRepository Teams { get; }

        IPlayerRepository Players { get; }

        IRepository<TeamByCompetition> TeamByCompetition { get; }
        Task<int> Save();
    }
}
