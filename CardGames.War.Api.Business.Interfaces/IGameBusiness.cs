using CardGames.War.Api.Models;

namespace CardGames.War.Api.Business.Interfaces
{
    /// <summary>
    /// Defines the possible business methods for the business entity.
    /// </summary>
    public interface IGameBusiness
    {
        /// <summary>
        /// Connects to database and transform some extracted database.
        /// </summary>
        /// <param name="newGameData">Required data to create a new game.</param>
        /// <returns>Game identifier generated.</returns>
        public int NewGame(NewGameModel newGameData);

        /// <summary>
        /// Returns the score for an specific user.
        /// </summary>
        /// <param name="userEmail"></param>
        /// <param name="playerId"></param>
        /// <returns></returns>
        public ScoreModel GetScore(string userEmail, string playerId);
    }
}
