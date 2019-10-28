using ImportLeague.Core.Entities;
using System.Threading.Tasks;

namespace ImportLeague.Core.Interfaces.Services
{
    public interface ICompetitionService
    {
        Task<bool> AlreadyImported(string code);
        Task ImportLeague(Competition competition);
        Task<Competition> GetCompetition(string leagueCode);
        Task<int> CountPlayersByCompetition(string leagueCode);
    }
}
