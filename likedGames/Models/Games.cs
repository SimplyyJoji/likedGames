using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace likedGames.Models
{
    public class Game
    {
        [Key] // denotes PK, not needed if named ModelNameId
        public int GameId { get; set; }

        [Required(ErrorMessage = "is required.")]
        [MinLength(2, ErrorMessage = "must be at least 2 characters")]
        [Display(Name = "Title")]
        public string Title { get; set; }

        [Required(ErrorMessage = "is required.")]
        [MinLength(2, ErrorMessage = "must be at least 2 characters")]
        [Display(Name = "Description")]
        public string Description { get; set; }

        [Required(ErrorMessage = "is required.")]
        [Display(Name = "Rating")]
        public string Rating { get; set; } 

        [Required(ErrorMessage = "is required.")]
        [Display(Name = "Media Url")]
        public string Src { get; set; }
        
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;


        public List <LikedGame> likedGames { get; set; }
        public int UserId { get; set; }
        public User CreatedBy { get; set; }
    }
}