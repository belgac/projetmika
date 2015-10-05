using magicManager.DAL;
using magicManager.Models.Expansions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace magicManager.Controllers
{
    public class ExpansionController : Controller
    {
        // GET: Expansion
        public ActionResult ExpansionRequest()
        {

            IExpansionRepository ExpRepo = new ExpansionRepository();

            IEnumerable<Expansion> result = ExpRepo.GetExpansion();
            SelectList list = new SelectList(result, "idExpansion", "name");
            ViewBag.ddl = list;
            if (result != null)
            { 
                return View(result.ToList());
            }
            return View();
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult GetExpansionById(string idGame)
        {
            if (String.IsNullOrEmpty(idGame))
            {
                throw new ArgumentNullException("idExpansion");
            }
            int id = 0;
            bool isValid = Int32.TryParse(idGame, out id);
            IExpansionRepository ExpRepo = new ExpansionRepository();

            IEnumerable<Expansion> result = ExpRepo.GetExpansionByID(id);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

    }



}