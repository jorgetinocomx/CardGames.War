using Microsoft.EntityFrameworkCore;
using CardGames.War.Api.Entities;


namespace CardGames.War.Api.DataAccess
{
    /// <summary>
    /// DB context used for the API database.
    /// </summary>
    public class ApiDbContext : DbContext
    {
        public ApiDbContext(DbContextOptions<ApiDbContext> options) : base(options)
        {

        }
        /// <summary>
        /// Represents the games entries into the database.
        /// </summary>
        public DbSet<Game> Games { get; set; }

    }
}
