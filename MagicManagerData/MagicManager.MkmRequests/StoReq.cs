using MagicManager.dal.Repositories;
using MagicManager.DAL.Repositories;
using MagicManager.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MagicManager.MkmRequests
{
    public class StoReq
    {

        public static IEnumerable<ArticleMkm> StockRequest()
        {
            //Retourne l'ensemble du stock de l'user. 
            //utile pour suivi des ventes et statistiques.
            //utile pour configuration via indice de priorité
            string method = "GET";
            string url = "https://www.mkmapi.eu/ws/v1.1/output.json/stock";

            HttpWebRequest request = WebRequest.CreateHttp(url) as HttpWebRequest;
            OAuthHeader header = new OAuthHeader();
            request.Headers.Add(HttpRequestHeader.Authorization, header.getAuthorizationHeader(method, url));
            request.Method = method;

            HttpWebResponse response = request.GetResponse() as HttpWebResponse;
            string text = Utils.StreamToText(response);

            if (text == null) return null;
            RootArticle root = JsonConvert.DeserializeObject<RootArticle>(text);

            var collection = root.article as IEnumerable<ArticleMkm>;
            
            ArticleRepo arRepo = new ArticleRepo();
            LangRepo laRepo = new LangRepo();
            DailyPriceRepo dpRepo = new DailyPriceRepo();    

            foreach (ArticleMkm article in collection)
            {
                MagicManager.Model.Lang cur = laRepo.FindBy(l => l.LanguageId == article.language.idLanguage).FirstOrDefault();
                if (cur == null)
                {
                    MagicManager.Model.Lang mylang = new MagicManager.Model.Lang();
                    mylang.LanguageId = article.language.idLanguage;
                    mylang.Name = article.language.languageName;
                    laRepo.Add(mylang);
                    laRepo.Save();
                }

                Article curAr = arRepo.FindBy(a => a.ArticleId == article.idArticle).FirstOrDefault();
                if (curAr == null)
                {
                    curAr = new Article();
                    curAr.ArticleId = (article.idArticle *1);
                    curAr.ProductId = article.idProduct;
                    curAr.LanguageId = article.language.idLanguage;
                    curAr.isFoil = article.isFoil;
                    curAr.isSigned = article.isSigned;
                    curAr.isPlayset = article.isPlayset;
                    curAr.isAltered = article.isAltered;
                    curAr.isFirstEd = article.isFirstEd;
                    curAr.Count = article.count;
                    //todo : change price in our db to have a double
                    curAr.Price = (int)article.price;
                    curAr.WorkerEditTime = DateTime.Now;
                    arRepo.Add(curAr);
                }
                else
                {
                    curAr.ProductId = article.idProduct;
                    curAr.LanguageId = article.language.idLanguage;
                    curAr.isFoil = article.isFoil;
                    curAr.isSigned = article.isSigned;
                    curAr.isPlayset = article.isPlayset;
                    curAr.isAltered = article.isAltered;
                    curAr.isFirstEd = article.isFirstEd;
                    curAr.Count = article.count;
                    curAr.WorkerEditTime = DateTime.Now;
                }

                DailyPrice dp = dpRepo.FindBy(d => d.Productid == curAr.ProductId).OrderBy(d => d.WorkerEditTime).FirstOrDefault();
                if (dp != null)
                {
                    dp.Price = article.price;
                }
                arRepo.Save();
                dpRepo.Save();
            }
            return collection;
        }

    }
}
