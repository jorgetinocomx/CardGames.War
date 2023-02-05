using CardGames.War.Api.DataAccess.Interfaces;
using CardGames.War.Api.Entities;

namespace CardGames.War.Api.DataAccess
{
    /// <summary>
    /// In charge of get the information directly from the database.
    /// </summary>
    /// <remarks>
    /// This implementation uses MS SQL server as the DB provider.
    /// </remarks>
    public class GameData: IGameData
    {
        private readonly ApiDbContext _context;

        /// <summary>
        /// Injects the ApiDbContext.
        /// </summary>
        /// <param name="context">Created instance for the context.</param>
        public GameData(ApiDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// IMPLEMENTATION: Inserts a record in the Game table.
        /// </summary>
        /// <param name="newGameData">Required data to create a new game/record.</param>
        public Game NewGame(Game newGameData)
        {
            _context
                    .Games
                    .Add(newGameData);
            _context.SaveChanges();
            return newGameData;
        }

        /// <summary>
        /// IMPLEMENTATION: Returns the games where the playerID won.
        /// </summary>
        /// <param name="userEmail">User requesting the information.</param>
        /// <param name="playerId">The information was requested for this user.</param>
        /// <returns>All won games for the specified player ID.</returns>
        public IEnumerable<Game> GetWonGames(string userEmail, string playerId)
        {
            var itemsFound = _context
                               .Games
                               .Where(game => game.UserEmail == userEmail 
                                           && game.Winner == playerId
                                           && game.FinishDate != null)
                               .OrderByDescending(game => game.StartDate)
                               .AsEnumerable();
            return itemsFound;
        }

        /// <summary>
        /// Gets the IQueryable object (used to perform some operations).
        /// </summary>
        public IQueryable<Game> Get()
        {
           return _context.Games.AsQueryable(); 
        }


        /// <summary>
        /// Updates the data in the db.
        /// </summary>
        /// <param name="updatedData">Data previously updated.</param>
        public void Update(Game updatedData)
        {
            _context
                .Games.Update(updatedData);
            _context.SaveChanges();
        }
    }
}
