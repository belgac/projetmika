using MagicManager.dal.Repositories;
using MagicManager.Model;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagicManager.MkmRequests
{
    public class DailyPriceUpdate 
    {

        //Option for job :
        //int request = 0;
        //while (request > 50 000)
        //    do {
        //        DailyPriceUpdate;
        //        startwith(product orderby (DateTime.now -workerEditTime )
        //        request ++;
        //    }

        public static void DailyPriceRequest()
        {

            ProductRepo proRepo = new ProductRepo();
            DailyPriceRepo dpRepo = new DailyPriceRepo();

            foreach (Product prod in proRepo.GetAll())
            {
                ProductMkm productMkm = ProdReq.ProductRequest(prod.ProductId);
                DailyPrice dailyPrice = new DailyPrice();
                dailyPrice.Average = productMkm.priceGuide.AVG;
                dailyPrice.CountArticles = productMkm.countArticles;
                dailyPrice.CountFoils = productMkm.countFoils;
                dailyPrice.Low = productMkm.priceGuide.LOW;
                dailyPrice.Productid = prod.ProductId;
                dailyPrice.Sell = productMkm.priceGuide.SELL;
                dailyPrice.WorkerEditTime = DateTime.Now;

                dpRepo.Add(dailyPrice);
                dpRepo.Save();
            }

        }
    
    }
}




