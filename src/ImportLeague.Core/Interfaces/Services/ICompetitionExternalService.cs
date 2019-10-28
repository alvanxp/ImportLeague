using ImportLeague.Core.Entities;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ImportLeague.Core.Interfaces.Services
{
    public interface ICompetitionExternalService
    {
        Task<Competition> GetCompetition(string code, CancellationTokenSource cancellationToken);
        Task<IEnumerable<Player>> GetPlayers(string code, long id, CancellationTokenSource token);
        Task<IEnumerable<Team>> GetCompetitionAndTeams(string code, CancellationTokenSource token);
    }
}
