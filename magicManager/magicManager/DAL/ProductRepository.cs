using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using magicManager.Models;
using Newtonsoft.Json;
using magicManager.Infrastructure;
using Newtonsoft.Json.Linq;
using magicManager.Models.Articles;

namespace magicManager.DAL
{
    public class ProductRepository : IProductRepository
    {
        RequestHelper essai = new RequestHelper();

        public void DeleteProduct(int idProduct)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public Product GetProduct()
        {
            string text = essai.ProductRequest(1);
            if (text == null) return null;
            Product root = JsonConvert.DeserializeObject<Product>(text,new MyFrackinJsonConverter());
            return root as Product;
        }

        //From documentation on MarketPlace Information in V1.1 (That should be fine since we use Oauth)
        public IEnumerable<Product> GetProductByExpansion(int idGame, string expansionName)
        {
            string text = essai.ProductInExpansionRequest(idGame, expansionName);
            if (text == null) return null;
            JObject jObject = JObject.Parse(text);
            List<Product> products = new List<Product>();

            IStockRepository stockRepo = new StockRepository();
            IEnumerable<Article> articles = stockRepo.GetStock();
            foreach(JObject prd in jObject["card"])
            {
                Product product = JsonConvert.DeserializeObject<Product>(prd.ToString().Replace("\r\n",""), new MyFuckingJsonConverter());
                int id = product.idProduct;
                product = GetProductById(id);
                if (articles.Where(a => a.idProduct == id).Count() > 0)
                { 
                    product.myPrice = articles.Where(a => a.idProduct == id).FirstOrDefault<Article>().price;
                    product.inStock = true;
                }
                else
                {
                    product.inStock = false;
                }
                products.Add(product);
            }

            
            return products.OrderBy(m => m.name.productName) as IEnumerable<Product>;
        }

        public Product GetProductById(int idProduct)
        {
            string text = essai.ProductRequest(idProduct);
            if (text == null) return null;
            Product root = JsonConvert.DeserializeObject<Product>(text, new MyFrackinJsonConverter());
            return root as Product;
        }

        public void InsertProduct(Product product)
        {
            throw new NotImplementedException();
        }

        public void Save()
        {
            throw new NotImplementedException();
        }

        public void UpdateProduct(Product product)
        {
            throw new NotImplementedException();
        }
    }
}