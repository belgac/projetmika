using MagicManager.dal.Repositories;
using MagicManager.MkmRequests.Infrastructure;
using MagicManager.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MagicManager.MkmRequests
{
    public class ProdExpReq
    {
        public static IEnumerable<Product> ProductInExpansionRequest(Expansion expansion)
        {
            //retourne la liste des articles contenus dans l'expansion spécifiée du jeu spécifié.
            //par défaut l'idGame peut être mis sur "1", index de MTG, jeu phare du projet
            string method = "GET";
            string url = "https://www.mkmapi.eu/ws/v1.1/output.json/expansion/" + expansion.GameId + "/" + expansion.Name;

            HttpWebRequest request = WebRequest.CreateHttp(url) as HttpWebRequest;
            OAuthHeader header = new OAuthHeader();
            request.Headers.Add(HttpRequestHeader.Authorization, header.getAuthorizationHeader(method, url));
            request.Method = method;

            HttpWebResponse response = request.GetResponse() as HttpWebResponse;
            string text = Utils.StreamToText(response);

            if (text == null) return null;
            JObject jObject = JObject.Parse(text);
            
            //dans magicmanager original, cette méthode faisait la comparaison du prix MKM avec le prix magasin du user.
            //Il était donc nécessaire d'appeler le stock pour vérifier si la carte existait, ainsi que son prix.
            //IStockRepository stockRepo = new StockRepository();
            //IEnumerable<Article> articles = stockRepo.GetStock();

            ProductRepo prRepo = new ProductRepo();
            
            foreach (JObject prd in jObject["card"])
            {
                ProductMkm prod = JsonConvert.DeserializeObject<ProductMkm>(prd.ToString().Replace("\r\n", ""), new MyFuckingJsonConverter());
                Product curPro = prRepo.FindBy(p => p.ProductId == prod.idProduct).FirstOrDefault();

                if (curPro == null)
                {
                    curPro = new Product();
                    curPro.ProductId = prod.idProduct;
                    curPro.ProductName = prod.name.productName;
                    curPro.ProductUrl = prod.website;
                    curPro.ImageUrl = prod.image;
                    curPro.ExpansionId = expansion.ExpansionId;
                    curPro.WorkerEditTime = DateTime.Now;
                    curPro.Rarity = prod.rarity;
                    prRepo.Add(curPro);
                }
                else
                {
                    curPro.ProductName = prod.name.productName;
                    curPro.ProductUrl = prod.website;
                    curPro.ImageUrl = prod.image;
                    curPro.Rarity = prod.rarity;
                    curPro.WorkerEditTime = DateTime.Now;
                }
                prRepo.Save();
            }

            return prRepo.GetAll();
        }
    }
}
