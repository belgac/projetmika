using magicManager.DAL;
using magicManager.Models;
using magicManager.Models.Articles;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace magicManager.Controllers
{
    public class ArticleController : Controller
    {
        public ActionResult ArticleRequest()
        {
            IArticleRepository ArtRepo = new ArticleRepository();

            IEnumerable<Article> result = ArtRepo.GetArticles();

            if (result != null)
            {
                return View(result.ToList());
            }
            return View();
        }

    }
}