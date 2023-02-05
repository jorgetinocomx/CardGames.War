using CardGames.War.Api.Business.Interfaces;
using CardGames.War.Api.DataAccess.Interfaces;
using CardGames.War.Api.Entities;
using CardGames.War.Api.Models;

namespace CardGames.War.Api.Business
{
    /// <summary>
    /// Implements all the current business rules that applies to the games.
    /// </summary>
    public class GameBusiness : IGameBusiness
    {
        private IGameData _dataAccess;

        /// <summary>
        /// Inject the data access instance.
        /// </summary>
        /// <param name="dataAccess">Instanced data access.</param>
        public GameBusiness(IGameData dataAccess)
        {
            _dataAccess = dataAccess;
        }

        /// <summary>
        /// IMPLEMENTATION: Returns the score for an specific user.
        /// </summary>
        /// <param name="userEmail"></param>
        /// <param name="playerId"></param>
        /// <returns></returns>
        public ScoreModel GetScore(string userEmail, string playerId)
        {
            var rawData = _dataAccess.GetWonGames(userEmail, playerId);
            var winHistory = new List<DateTime>();
            foreach (var item in rawData)
            {
                winHistory.Add(item.FinishDate.Value);
            }
            var score = new ScoreModel() { UserEmail = userEmail, WonDates = winHistory, PlayerName = playerId };
            return score;
        }


        /// <summary>
        /// IMPLEMENTATION: Connects to database and start/create a new game.
        /// </summary>
        /// <param name="newGameData">Required data to create a new game.</param>
        /// <returns>Game identifier generated.</returns>
        public int NewGame(NewGameModel newGameData)
        {
            var entityToBeStored = new Game()
            {
                UserEmail = newGameData.UserEmail,
                StartDate = DateTime.Now,
            };
            var storedEntity = _dataAccess.NewGame(entityToBeStored);
            return storedEntity.Id;
        }


        /// <summary>
        /// IMPLEMENTATION: Mark a game as finished and sets the winner.
        /// </summary>
        /// <param name="gameId">Game identifier.</param>
        /// <param name="winner">Player ID who won the game.</param>
        public void FinishGame(int gameId, string winner)
        {
            var entityToBeUpdated = _dataAccess
                                        .Get()
                                        .FirstOrDefault(game => game.Id == gameId);
            if (entityToBeUpdated != null) {
                entityToBeUpdated.Winner = winner;
                entityToBeUpdated.FinishDate = DateTime.Now;
                _dataAccess.Update(entityToBeUpdated);
            }
        }
    }
}
