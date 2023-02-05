namespace CardGames.War.Api.Models
{
    /// <summary>
    /// Has all the required values to start a new game.
    /// </summary>
    public class NewGameModel
    {
        /// <summary>
        /// User who is running the game.
        /// </summary>
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public string UserEmail { get; set; }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    }
}
