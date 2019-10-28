using AutoMapper;
using ImportLeague.Core.Entities;
using ImportLeague.Core.Interfaces.Services;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace ImportLeague.Infrastructure
{
    public class CompetitionExternalService : BaseExternalService, ICompetitionExternalService
    {
        public CompetitionExternalService(IHttpClientFactory httpClientFactory, IMapper mapper) :
            base(httpClientFactory, mapper)
        {
        }

        public async Task<Competition> GetCompetition(string code, CancellationTokenSource cancellationToken)
        {
            var response = await base.Get<ExternalModels.Competition>($"competitions/{code}", cancellationToken);

            if (response != null)
            {
                return Mapper.Map<ExternalModels.Competition, Competition>(response);
            }

            return null;
        }

        public async Task<IEnumerable<Team>> GetCompetitionAndTeams(string code, CancellationTokenSource cancellationToken)
        {
            var response = await base.Get<ExternalModels.Competition>($"competitions/{code}/teams?limit=10", cancellationToken);

            if (response != null && response.Teams != null)
            {
                return Mapper.Map<IEnumerable<ExternalModels.Team>, IEnumerable<Team>>(response.Teams);
            }

            return Enumerable.Empty<Team>();
        }

        public async Task<IEnumerable<Player>> GetPlayers(string code, long id, CancellationTokenSource token)
        {
            var response = await base.Get<ExternalModels.Team>($"teams/{id}", token);

            if (response != null && response.Squad != null)
            {
                response.Squad.ForEach(p => p.TeamId = id);
                return Mapper.Map<IEnumerable<ExternalModels.Player>, IEnumerable<Player>>(response.Squad);
            }

            return Enumerable.Empty<Player>();
        }
    }
}
