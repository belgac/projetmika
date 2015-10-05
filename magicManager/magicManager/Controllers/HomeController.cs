using magicManager.DAL;
using magicManager.Models;
using magicManager.Models.Account;
using magicManager.Models.Articles;
using magicManager.Models.Expansions;
using magicManager.Models.Game;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Mvc;

namespace magicManager.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult GameRequest()
        {
            //GET GAME
            IGameRepository GameRepo = new GameRepository();

            IEnumerable<Game> result = GameRepo.GetGame();
            SelectList list = new SelectList(result,"idGame","name");
            ViewBag.ddl = list;
            if (result != null)
            {
                return View(result.ToList());
            }
            return View();
        }

    }
}