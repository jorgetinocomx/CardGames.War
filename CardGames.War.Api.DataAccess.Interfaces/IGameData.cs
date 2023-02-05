using CardGames.War.Api.Entities;

namespace CardGames.War.Api.DataAccess.Interfaces
{
    /// <summary>
    /// Defines the interfaces/contracts for the game data access.
    /// </summary>
    public interface IGameData
    {
        /// <summary>
        /// Inserts a record in the Game table.
        /// </summary>
        /// <param name="newGameData">Required data to create a new game/record.</param>
        public Game NewGame(Game newGameData);

        /// <summary>
        /// Returns the games where the playerID won.
        /// </summary>
        /// <param name="userEmail">User requesting the information.</param>
        /// <param name="playerId">The information was requested for this user.</param>
        /// <returns>All won games for the specified player ID.</returns>
        public IEnumerable<Game> GetWonGames(string userEmail, string playerId);

        /// <summary>
        /// Gets the IQueryable object (used to perform some operations).
        /// </summary>
        public IQueryable<Game> Get();

        /// <summary>
        /// Updates the data in the db.
        /// </summary>
        /// <param name="updatedData">Data previously updated.</param>
        public void Update(Game updatedData);

    }
}
