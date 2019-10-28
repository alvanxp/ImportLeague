using ImportLeague.Api.Models;
using ImportLeague.Core.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace ImportLeague.Api.Controllers
{
    /// <summary>
    /// Players
    /// </summary>
    [Route("total-players")]
    [ApiController]
    public class PlayersController : ControllerBase
    {
        private readonly ICompetitionService _competitionService;

        public PlayersController(ICompetitionService competitionService)
        {
            _competitionService = competitionService;
        }

        /// <summary>
        /// Get Total number of players by League
        /// </summary>
        /// <param name="leagueCode"></param>
        /// <returns></returns>
        [HttpGet("{leagueCode}")]
        [ProducesResponseType(typeof(PlayerCount), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Result), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(Result), StatusCodes.Status504GatewayTimeout)]
        public async Task<IActionResult> Get([Required] string leagueCode)
        {
            var exists = await _competitionService.AlreadyImported(leagueCode);
            if (!exists) return NotFound(new Result("Not found"));

            int count = await _competitionService.CountPlayersByCompetition(leagueCode);
            return Ok(new PlayerCount(count));
        }
    }
}
