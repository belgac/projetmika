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
        public static void DailyPriceRequest()
        {

            int dailyrequest = 0;

            ProductRepo proRepo = new ProductRepo();
            DailyPriceRepo dpRepo = new DailyPriceRepo();
            StockInfoRepo stoRepo = new StockInfoRepo();
            MarketInfoRepo marRepo = new MarketInfoRepo();
            ArticleRepo arRepo = new ArticleRepo();
            MarketInfo curMarket = new MarketInfo();
            StockInfo curstock = new StockInfo();

            /*.Where(p => p.WorkerEditTime.Value.Day != DateTime.Now.Day)*/
            foreach (Product prod in proRepo.GetAll().ToList().OrderBy(p => p.WorkerEditTime))
                {
                    ProductMkm productMkm = new ProductMkm();

                //this condition is imposed by the constraints of the API : a commercial site can go up to 50k request/day.
                if (dailyrequest < 50000)
                {
                    productMkm = ProdReq.ProductRequest(prod.ProductId);
                    dailyrequest++;
                }

                if (productMkm != null)
                    {
                        DailyPrice dailyPrice = new DailyPrice();
                        dailyPrice.Average = productMkm.priceGuide.AVG;
                        dailyPrice.CountArticles = productMkm.countArticles;
                        dailyPrice.CountFoils = productMkm.countFoils;
                        dailyPrice.Low = productMkm.priceGuide.LOW;
                        dailyPrice.Productid = prod.ProductId;
                        dailyPrice.Sell = productMkm.priceGuide.SELL;
                        dailyPrice.WorkerEditTime = DateTime.Now;

                        var curAr = arRepo.FindBy(a => a.ProductId == prod.ProductId).FirstOrDefault();
                        if (curAr != null)
                        {
                            dailyPrice.Price = curAr.Price;
                        }

                        //to do : check if calcul possible in this form or need to be formatted
                        if (dailyPrice.Price == null || dailyPrice.Average == null)
                        {
                            dailyPrice.Delta = 0;
                        }
                        else
                        { 
                            dailyPrice.Delta = (dailyPrice.Price / dailyPrice.Average )-1 ;
                        }

                    dailyPrice.AbsoluteDelta = (dailyPrice.Delta >= 0 ? dailyPrice.Delta : (dailyPrice.Delta * -1));

                        dpRepo.Add(dailyPrice);
                        dpRepo.Save();

                        //condition pour vérifier si l'article est en stock, afin de l'ajouter au curStock
                        if (arRepo.FindBy(a => a.ProductId == productMkm.idProduct).FirstOrDefault() != null)
                        {
                            curstock.Sell += dailyPrice.Sell;
                            curstock.Low += dailyPrice.Low;
                            curstock.Average += dailyPrice.Average;
                        }
                        //useless but to remind : 
                        //else
                        //{
                        //    curstock.Sell += 0;
                        //    curstock.Low += 0;
                        //    curstock.Average += 0;
                        //}

                        curMarket.Sell += dailyPrice.Sell;
                        curMarket.Low += dailyPrice.Low;
                        curMarket.Average += dailyPrice.Average;

                    prod.WorkerEditTime = DateTime.Now;
                    proRepo.Save();
                    }
                }
            curstock.WorkerEditTime = DateTime.Now;
            stoRepo.Add(curstock);
            stoRepo.Save();

            curMarket.WorkerEditTime = DateTime.Now;
            marRepo.Add(curMarket);
            marRepo.Save();

        }
    
    }
}
