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
    public class WatchListController : ApiController
    {
        public IHttpActionResult Get(int id)
        {
            DailyPriceRepo dpRepo = new DailyPriceRepo();
            var watchDp = dpRepo.GetAll().Where(d => d.WorkerEditTime.Value.Day == DateTime.Now.Day).OrderBy(d => d.Delta);
            ProductRepo prRepo = new ProductRepo();

            List<Product> watchlist = new List<Product>;

            foreach (var prod in watchDp)
            {
                if (prod.Delta > 0)
                {
                    if (prod.Price > 0.25 && prod.Price < 2.49)
                    {
                        if (prod.Price > prod.Sell +1)
                        {
                            watchlist.Add(prRepo.FindBy(p => p.ProductId == prod.Productid).FirstOrDefault());
                        }
                    }
                    else if (prod.Price > 2.49 && prod.Price < 4.99)
                    {
                        if (prod.Price > prod.Sell + 1.5)
                        {
                            watchlist.Add(prRepo.FindBy(p => p.ProductId == prod.Productid).FirstOrDefault());
                        }
                    }
                    else if (prod.Price > 5 && prod.Price < 10)
                    {
                        if (prod.Price > prod.Sell + 3)
                        {
                            watchlist.Add(prRepo.FindBy(p => p.ProductId == prod.Productid).FirstOrDefault());
                        }
                    }
                    else if (prod.Price > 10 && prod.Price < 15)
                    {
                        if (prod.Price > prod.Sell + 4)
                        {
                            watchlist.Add(prRepo.FindBy(p => p.ProductId == prod.Productid).FirstOrDefault());
                        }
                    }
                    else if (prod.Price > 15)
                    {
                        if (prod.Price > prod.Sell * 1.5)
                        {
                            watchlist.Add(prRepo.FindBy(p => p.ProductId == prod.Productid).FirstOrDefault());
                        }
                    }
                }
            }

            if (watchlist == null)
            {
                return NotFound();
            }
            return Ok(watchlist);
        }
    }
}
