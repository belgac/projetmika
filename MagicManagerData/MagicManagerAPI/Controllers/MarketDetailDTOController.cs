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
    public class MarketDetailDTOController : ApiController
    {
        public IHttpActionResult Get()
        {
            var prRepo = new ProductRepo();
            var dpRepo = new DailyPriceRepo();

            MarketDetailDTO mktDTO = new MarketDetailDTO();
            List<Product> prd = prRepo.GetAll().ToList();
            List<DailyPrice> dp = new List<DailyPrice>();
                
            foreach (Product p in prd)
            {
               var lastDp = dpRepo.FindBy(d => d.Productid == p.ProductId).OrderBy(d => d.WorkerEditTime).FirstOrDefault();
                if (p != null && lastDp != null)
                {
                    mktDTO.TopProducts.Add(p, lastDp);
                }
            } 

            if (mktDTO.TopProducts == null)
            {
                Console.WriteLine("Oups");
                return NotFound();
            }
            Console.WriteLine("hello, success");
            return Ok(mktDTO);
        }
    }
}
