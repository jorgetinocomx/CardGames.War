using CardGames.War.Api.Business.Interfaces;
using CardGames.War.Api.Models;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace CardGames.War.Api.Controllers
{
    /// <summary>
    /// Manages the games in the card games system.
    /// </summary>
    [ApiController]
    [Route("api/game")]
    public class GameController : Controller
    {
        private readonly ILogger<GameController> _logger;
        private readonly IGameBusiness _business;

        /// <summary>
        /// Inject the dependencies.
        /// </summary>
        /// <param name="logger">MS logger dependency instanciated.</param>
        /// <param name="business">Game business dependency instanciated.</param>
        public GameController(ILogger<GameController> logger, IGameBusiness business)
        {
            _logger = logger;
            _business = business;
        }

        /// <summary>
        /// Get all the games won for an specific user.
        /// </summary>
        /// <param name="userEmail">User email that is requesting the score.</param>
        /// <param name="playerId">Player ID that will be used to get his/her lifetime wins.</param>
        /// <returns>Lifetime wins for an specific player.</returns>
        [HttpGet]
        [Route("score")]
        [Produces("application/json")]
        public ScoreModel Get([EmailAddress] string userEmail, string playerId)
        {
            _logger.LogDebug($"Requested score for player : {playerId} - Requested by {userEmail}");
            var lifetimeWins = _business.GetScore(userEmail, playerId);
            return lifetimeWins;
        }

        /// <summary>
        /// Creates a new game.
        /// </summary>
        /// <param name="newGameData">Required data to craete a new game.</param>
        /// <returns>ok when game was create successfully.</returns>
        [HttpPost()]
        public IActionResult New(NewGameModel newGameData)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            var generatedGameID  = _business.NewGame(newGameData);
            _logger.LogDebug($"The game{generatedGameID} was created for the user : {newGameData.UserEmail}");
            return Ok(generatedGameID);
        }
    }
}
