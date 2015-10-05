using magicManager.DAL;
using magicManager.Models;
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
    public class ProductController : Controller
    {
        public ActionResult ProductRequest()
        {
            IProductRepository ProdRepo = new ProductRepository();

            Product result = ProdRepo.GetProduct();

            if(result != null)
            {
                return View(result);
            }
            return View();
        }

        public ActionResult ProductInExpansionRequest(int idGame, string expansionName)
        {
            IProductRepository ProdRepo = new ProductRepository();

            IEnumerable<Product> result = ProdRepo.GetProductByExpansion(idGame, expansionName.Replace(" ", "%20"));

            if (result != null)
            {
                return View("ProductRequest",result.ToList());
            }
            return View("ProductRequest");
        }

        [OutputCache(Duration = 30, VaryByParam = "idProduct")]
        public ActionResult _ProductPartial(int idProduct)
        {
            IProductRepository ProdRepo = new ProductRepository();

            Product result = ProdRepo.GetProductById(idProduct);

            if (result != null)
            {
                return PartialView(result);
            }
            return PartialView();
        }
    }
}