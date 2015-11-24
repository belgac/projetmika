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
    public class ProdReq
    {
        public static ProductMkm ProductRequest(int id)
        {
            //retourne le produit spécifié par l'Id. Pour la différence complête product/article, 
            //se reporter à la documentation ou à l'API MKM.
            string method = "GET";
            string url = "https://www.mkmapi.eu/ws/v1.1/output.json/product/" + id;

            HttpWebRequest request = WebRequest.CreateHttp(url) as HttpWebRequest;
            OAuthHeader header = new OAuthHeader();
            request.Headers.Add(HttpRequestHeader.Authorization, header.getAuthorizationHeader(method, url));
            request.Method = method;

            HttpWebResponse response = request.GetResponse() as HttpWebResponse;
            string text = Utils.StreamToText(response);

            if (text == null) return null;


            JObject jObject = JObject.Parse(text);
            ProductMkm prod = JsonConvert.DeserializeObject<ProductMkm>(jObject["product"].ToString().Replace("\r\n", ""), new MyFuckingJsonConverter());

           // JsonConvert.DeserializeObject<ProductMkm>(jObject["product"].ToString());

            //this will create a new product in our db, with the informations of the productMkm returned by the request.
            ProductRepo prRepo = new ProductRepo();
            Product curPro = prRepo.FindBy(a => a.ProductId == prod.idProduct).FirstOrDefault();

            if (curPro == null)
            {
                Product myProd = new Product();
                myProd.ProductId = prod.idProduct;
                myProd.ProductName = prod.name.productName;
                myProd.ProductUrl = prod.website;
                myProd.ImageUrl = prod.image;
                myProd.ExpansionId = prod.expIcon;
                //rarity : check parser
                myProd.Rarity = prod.rarity;
                myProd.WorkerEditTime = DateTime.Now;

                prRepo.Add(myProd);
                prRepo.Save();
            }
            else
            {
                curPro.WorkerEditTime = DateTime.Now;
                prRepo.Save();
            }

            return prod;
        }
    }
}
