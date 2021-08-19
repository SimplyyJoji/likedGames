using Microsoft.EntityFrameworkCore;

namespace likedGames.Models
{
    public class likedGameContext : DbContext
    {
        public likedGameContext(DbContextOptions options) : base(options) { }
 
        // for every model / entity that is going to be part of the db
        // the names of these properties will be the names of the tables in the db
        public DbSet<User> Users { get; set; }
        public DbSet<Game> Gamess { get; set; }
        public DbSet<LikedGame> LikedGames { get; set; }
    }

    }
