namespace CardGames.War.Api.Models
{
    /// <summary>
    /// Represents the score for an specific user.
    /// </summary>
    public class ScoreModel
    {
        /// <summary>
        /// User requesting the score.
        /// </summary>
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public string UserEmail { get; set; }

        /// <summary>
        /// Identifies the player that ownes this score.
        /// </summary>
        public string PlayerName { get; set; }

        /// <summary>
        /// Represents the date when this player won a game.
        /// </summary>
        public IEnumerable<DateTime> WonDates { get; set; }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

    }
}
