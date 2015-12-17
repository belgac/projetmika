using MagicManager.dal.Repositories;
using MagicManager.MkmRequests;
using MagicManager.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MagicManager.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var repo = new ArticleRepo();
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

        public ActionResult PopulateGame()
        {
            GamReq.GameRequest();
            return View();
        }

        public ActionResult PopulateExpansion()
        {
            ExpReq.ExpansionRequest(1);
            return View("Index");
        }

        public ActionResult PopulateProduct()
        {
            ExpansionRepo eRepo = new ExpansionRepo();
            Expansion exp = eRepo.FindBy(e => e.Name == "Zendikar").FirstOrDefault();
            ProdExpReq.ProductInExpansionRequest(exp);

            return View("Index");
        }

        public ActionResult PopulateProductTest()
        {
            ProdReq.ProductRequest(5234);
            Console.WriteLine(ProdReq.ProductRequest(5234));
            return View("Index");
        }

        public ActionResult PopulateArticles()
        {
            StoReq.StockRequest();
            DailyPriceUpdate.DailyPriceRequest();
            return View("Index");
        }



    }
}