using MagicManager.dal.Repositories;
using MagicManager.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MagicManagerAPI.Controllers
{
    public class StockDetailDTOController : ApiController
    {
        public IHttpActionResult Get()
        {
            var arRepo = new ArticleRepo();
            var dpRepo = new DailyPriceRepo();
            var prRepo = new ProductRepo();

            StockDetailDTO stdDTO = new StockDetailDTO();
            List<Article> Allart = arRepo.GetAll().ToList();
            List<Product> stock = new List<Product>();

            foreach (Article art in Allart)
            {
                if (art.Count > 0)
                {
                    Product st = prRepo.FindBy(p => p.ProductId == art.ProductId).FirstOrDefault();
                    stock.Add(st);
                }
            }

            foreach (Product p in stock)
            {
                var lastDp = dpRepo.FindBy(d => d.Productid == p.ProductId).OrderBy(d => d.WorkerEditTime).FirstOrDefault();
                stdDTO.inStock.Add(p, lastDp);
            }

            if (stdDTO == null)
            {
                return NotFound();
            }
            return Ok(stdDTO);

        }
    }
}
