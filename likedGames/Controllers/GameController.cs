using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using likedGames.Models;

namespace likedGames.Controllers
{
    public class GameController : Controller
    {
        private likedGameContext db;
        public GameController(likedGameContext context)
        {
            db = context;
        }
        private int? uid
        {
            get
            {
                return HttpContext.Session.GetInt32("UserId");
            }
        }
        private bool isLoggedIn
        {
            get
            {
                return uid != null;
            }
        }

        [HttpGet("/games/new")]
        public IActionResult New()
        {
            if (!isLoggedIn) {
                return RedirectToAction("Index", "Home");
            }

            return View("New");
        }
    //Create() talks to form asp-action make sure its the same
        [HttpPost("/games/create")]
        public IActionResult Create(Game newGame)
        {

            // Every time a form is submitted, check the validations.
            if (ModelState.IsValid == false)
            {
                // Go back to the form so error messages are displayed.
                return View("New");
            }

            // if (newGame.Date <= DateTime.Now)
            // {
            //     ModelState.AddModelError("Date", "must be in the future.");
            //     return View("New");
            // }

            // If any above custom errors were added, ModelState would now be invalid.

            // HttpContext.Session.SetString("NameOne", newWedding.NameOne);
            newGame.UserId = (int)uid;
            db.Games.Add(newGame);
            db.SaveChanges();

            return RedirectToAction("Dashboard");
        }

        [HttpGet("/Dashboard")]
        public IActionResult Dashboard()
        {
            if (!isLoggedIn) {
                return RedirectToAction("Index", "Home");
            }
            
            List<Game> allWeddings = db.Games

                .Include(game => game.CreatedBy) // hover over the param to see it's data type
                
                .Include(game => game.likedGames)
                .ToList();
            return View("Dashboard", allWeddings);
        }

        [HttpGet("/games/{gameId}")]
        public IActionResult Details(int gameId)
        {
            if (!isLoggedIn) {
                return RedirectToAction("Index", "Home");
            }


            Game game = db.Games
                .Include(game => game.CreatedBy)
                .Include(game => game.likedGames)
                // Include something from the last thing that was included.
                // Include the User from the likes (hover over like param to see data type)
                .ThenInclude(like => like.CreatedBy)
                .FirstOrDefault(l => l.GameId == gameId);


            if (game == null)
            {
                return RedirectToAction("Details");
            }

            return View("Details", game);  
        }



        [HttpPost("/games/{gameId}/like")]
        public IActionResult Like(int gameId)
        {
            if (!isLoggedIn)
            {
                return RedirectToAction("Index", "Home");
            }

            LikedGame likedGame = db.LikedGames
                .FirstOrDefault(rsvp => rsvp.UserId == (int)uid && rsvp.GameId == gameId);

            if (likedGame == null)
            {
                LikedGame like = new LikedGame()
                {
                    GameId = gameId,
                    UserId = (int)uid
                };

                db.LikedGames.Add(like);
            }
            else
            {
                db.LikedGames.Remove(likedGame);
            }


            db.SaveChanges();
            return RedirectToAction("Dashboard");
        }

    }
}