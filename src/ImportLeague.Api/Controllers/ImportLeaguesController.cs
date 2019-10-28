using ImportLeague.Api.Models;
using ImportLeague.Core.Entities;
using ImportLeague.Core.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace ImportLeague.Controllers
{
    /// <summary>
    /// Import League
    /// </summary>
    [Route("import-league")]
    [ApiController]
    public class ImportLeaguesController : ControllerBase
    {
        private readonly ICompetitionService _competitionService;
        public ImportLeaguesController(ICompetitionService competitionService)
        {
            _competitionService = competitionService;
        }

        /// <summary>
        /// Import League by Code
        /// </summary>
        /// <param name="leagueCode">League code</param>
        /// <returns></returns>
        [HttpGet("{leagueCode}")]
        [ProducesResponseType(typeof(Result), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(Result), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(Result), StatusCodes.Status409Conflict)]
        [ProducesResponseType(typeof(Result), StatusCodes.Status504GatewayTimeout)]
        public async Task<IActionResult> Get([Required] string leagueCode)
        {
            var exists = await _competitionService.AlreadyImported(leagueCode);
            if (exists) return Conflict(new Result("League already imported"));

            Competition competition = await _competitionService.GetCompetition(leagueCode);
            if (competition == null) return NotFound(new Result("Not found"));

            await _competitionService.ImportLeague(competition);
            return StatusCode(StatusCodes.Status201Created, new Result("Successfully imported"));
        }


    }
}
