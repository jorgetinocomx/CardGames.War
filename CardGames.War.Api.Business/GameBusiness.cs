using CardGames.War.Api.Business.Interfaces;
using CardGames.War.Api.Models;

namespace CardGames.War.Api.Business
{
    /// <summary>
    /// Implements all the current business rules that applies to the games.
    /// </summary>
    public class GameBusiness : IGameBusiness
    {

        public ScoreModel GetScore(string userEmail, string playerId)
        {
            throw new NotImplementedException();
        }

        public void NewGame(NewGameModel newGameData)
        {
            throw new NotImplementedException();
        }
    }
}
