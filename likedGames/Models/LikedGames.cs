using System;
using System.ComponentModel.DataAnnotations;

namespace likedGames.Models
{
    public class LikedGame
    {
        [Key] // the below prop is the primary key, [Key] is not needed if named with pattern: ModelNameId
        public int LikedId { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
        

        public int UserId { get; set; }
        public User CreatedBy { get; set; }


        public int GameId { get; set; }
        public Game Games { get; set; }
    }
}