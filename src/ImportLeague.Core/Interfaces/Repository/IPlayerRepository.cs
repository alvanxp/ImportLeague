using ImportLeague.Core.Entities;
using System.Threading.Tasks;

namespace ImportLeague.Core.Interfaces.Repository
{
    public interface IPlayerRepository : IRepository<Player>
    {
        Task<int> GetNroPlayersByCompetition(string leagueCode);
    }
}
