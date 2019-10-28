using ImportLeague.Core.Entities;
using ImportLeague.Core.Interfaces.Repository;
using ImportLeague.Core.Interfaces.Services;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ImportLeague.Core.Services
{
    public class CompetitionService : ICompetitionService
    {
        private readonly ICompetitionExternalService _competitionExternalService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<CompetitionService> _logger;
        private CancellationTokenSource _cancellationTokenSource;
        public CompetitionService(ICompetitionExternalService competitionExternalService,
            IUnitOfWork unitOfWork, ILogger<CompetitionService> logger)
        {
            _competitionExternalService = competitionExternalService;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }
        public async Task<bool> AlreadyImported(string code)
        {
            if (string.IsNullOrEmpty(code)) throw new ArgumentNullException(nameof(code));
            return await _unitOfWork.Competitions.Count(c => c.Code == code) > 0;
        }

        public async Task<int> CountPlayersByCompetition(string leagueCode)
        {
            return await _unitOfWork.Players.GetNroPlayersByCompetition(leagueCode);
        }

        public async Task<Competition> GetCompetition(string leagueCode)
        {
            if (string.IsNullOrEmpty(leagueCode)) throw new ArgumentNullException(nameof(leagueCode));
            _cancellationTokenSource = new CancellationTokenSource();
            return await _competitionExternalService.GetCompetition(leagueCode, _cancellationTokenSource);
        }

        public async Task ImportLeague(Competition competition)
        {
            if (competition == null) throw new ArgumentNullException(nameof(competition));

            _cancellationTokenSource = new CancellationTokenSource();
            
            var teams = (await _competitionExternalService.GetCompetitionAndTeams(competition.Code, _cancellationTokenSource))?.Take(9);//to avoid "Too many requests"
            HashSet<long> notImportedTeams = await _unitOfWork.Teams.GetNotImportedTeams(teams);
            teams = teams.Where(t => notImportedTeams.Contains(t.Id));
            var teamsByCompetition = from t in teams
                                     select new TeamByCompetition
                                     {
                                         CompetitionId = competition.Id,
                                         TeamId = t.Id
                                     };            

            var tasks = new List<Task<IEnumerable<Player>>>();
            foreach (var team in teams)
            {   
                tasks.Add(_competitionExternalService.GetPlayers(competition.Code, team.Id, _cancellationTokenSource));
            }
            try
            {
                var results = await Task.WhenAll(tasks);
                using (var transaction = _unitOfWork.BeginTrainsaction())
                {
                    try
                    {
                        await _unitOfWork.Competitions.Add(competition);
                        await _unitOfWork.Save();
                        await _unitOfWork.Teams.AddRange(teams);
                        await _unitOfWork.Save();
                        await _unitOfWork.TeamByCompetition.AddRange(teamsByCompetition);
                        await _unitOfWork.Save();
                        foreach (var result in results)
                        {
                            await _unitOfWork.Players.AddRange(result);
                            await _unitOfWork.Save();
                        }

                        transaction.Commit();
                    }
                    catch (Exception)
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
            catch (OperationCanceledException)
            {
                foreach (var task in tasks)
                {
                    _logger.LogDebug($"Task {task.Id} has status {task.Status}");
                }
                throw;
            }
        }
    }
}
