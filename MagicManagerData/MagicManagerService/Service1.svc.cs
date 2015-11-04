using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using MagicManager;
using MagicManager.dal.Repositories;
using System.Net;
using System.IO;
using MagicManager.Models;
using System.Security.Cryptography;
using System.Web.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using magicManager.Infrastructure;

namespace MagicManagerService
{
    // REMARQUE : vous pouvez utiliser la commande Renommer du menu Refactoriser pour changer le nom de classe "Service1" dans le code, le fichier svc et le fichier de configuration.
    // REMARQUE : pour lancer le client test WCF afin de tester ce service, sélectionnez Service1.svc ou Service1.svc.cs dans l'Explorateur de solutions et démarrez le débogage.
    public class Service1 : IService1
    {

        //ce service va servir à récupérer les datas de l'API MKM et à les enregistrer en DB locale
        //il faut convertir les données reçues pour les adapter à notre db

        private string StreamToText(HttpWebResponse response)
        {
            //méthode prévue initialement pour parser le JSON récupéré de l'API, peut être obsolète dans la nouvelle architecture
            if (response.StatusCode == HttpStatusCode.OK)
            {
                Stream resp = response.GetResponseStream();

                StreamReader reader = new StreamReader(resp);
                string text = reader.ReadToEnd();
                return text;
            }

            return null;
        }


        public string AccountRequest()
        {
            string method = "GET";
            string url = "https://www.mkmapi.eu/ws/v1.1/output.json/account";

            HttpWebRequest request = WebRequest.CreateHttp(url) as HttpWebRequest;
            OAuthHeader header = new OAuthHeader();
            request.Headers.Add(HttpRequestHeader.Authorization, header.getAuthorizationHeader(method, url));
            request.Method = method;

            HttpWebResponse response = request.GetResponse() as HttpWebResponse;
            // XmlDocument doc = new XmlDocument();
            // doc.Load(response.GetResponseStream());
            // proceed further

            return StreamToText(response);
        }

        public ArticleMkm ArticleRequest(int idArticle)
        {
            //retourne l'article spécifié via IdArticle
            string method = "GET";
            string url = "https://www.mkmapi.eu/ws/v1.1/output.json/articles/" + idArticle;

            HttpWebRequest request = WebRequest.CreateHttp(url) as HttpWebRequest;
            OAuthHeader header = new OAuthHeader();
            request.Headers.Add(HttpRequestHeader.Authorization, header.getAuthorizationHeader(method, url));
            request.Method = method;

            HttpWebResponse response = request.GetResponse() as HttpWebResponse;
            string text = StreamToText(response);

            if (text == null) return null;
            ArticleMkm arti = JsonConvert.DeserializeObject<ArticleMkm>(text);
            
                Article myArticle = new Article();
                myArticle.ArticleId = arti.idArticle;
                myArticle.ProductId = arti.idProduct;
                myArticle.isFoil = arti.isFoil;
                myArticle.isPlayset = arti.isPlayset;
                myArticle.isSigned = arti.isSigned;
                myArticle.LanguageId = arti.language.idLanguage;
                myArticle.Language.Name = arti.language.languageName;

                ProductMkm current = ProductRequest(arti.idProduct);
                ProductRepo prodRepo = new ProductRepo();
                ArticleRepo artRepo = new ArticleRepo();
                artRepo.Add(myArticle);
                //Product currentProd = prodRepo.FindBy(product => product.ProductId == myArticle.ProductId).FirstOrDefault();
                //currentProd.Rarity = 
                //countArticles not found, check parser
                //myArticle.SiteWideCount = current.countArticles + current.countFoils;
            
            return arti;
}

        public ExpansionMkm ExpansionRequest(int id)
        {

            //retourne l'expansion spécifié par l'id.
            //Utile pour dropdowns lists du projet beta, sera certainement utile pour la configuration user
            string method = "GET";
            string url = "https://www.mkmapi.eu/ws/v1.1/output.json/expansion/" + id;

            HttpWebRequest request = WebRequest.CreateHttp(url) as HttpWebRequest;
            OAuthHeader header = new OAuthHeader();
            request.Headers.Add(HttpRequestHeader.Authorization, header.getAuthorizationHeader(method, url));
            request.Method = method;

            HttpWebResponse response = request.GetResponse() as HttpWebResponse;
            string text = StreamToText(response);

            if (text == null) return null;
            ExpansionMkm exp = JsonConvert.DeserializeObject<ExpansionMkm>(text);

                Expansion myExp = new Expansion();
                myExp.ExpansionId = exp.idExpansion;
                myExp.Icon = exp.icon;
                myExp.Name = exp.name;

            ExpansionRepo eRepo = new ExpansionRepo();
            eRepo.Add(myExp);

            return exp;
        }

        public IEnumerable<GameMkm> GameRequest()
        {
            //retourne la liste des jeux au format Json
            string method = "GET";
            string url = "https://www.mkmapi.eu/ws/v1.1/output.json/games";

            HttpWebRequest request = WebRequest.CreateHttp(url) as HttpWebRequest;
            OAuthHeader header = new OAuthHeader();
            request.Headers.Add(HttpRequestHeader.Authorization, header.getAuthorizationHeader(method, url));
            request.Method = method;

            HttpWebResponse response = request.GetResponse() as HttpWebResponse;
            string text = StreamToText(response);

            if (text == null) return null;
            RootGame root = JsonConvert.DeserializeObject<RootGame>(text);
            return root.game as IEnumerable<GameMkm>;
        }

        public string GetData(int value)
        {


            ///Appel API MKM
            ///collection = reponse de mkm
            foreach (var item in collection)
            {
                //logic biz.
                repo.check(item.id);
                repo.Add(item);
            }
            repo.GetAll();
            return string.Format("You entered: {0}", value);
        }

        public IEnumerable<ProductMkm> ProductInExpansionRequest(int idGame, string expansionName)
        {
            //retourne la liste des articles contenus dans l'expansion spécifiée du jeu spécifié.
            //par défaut l'idGame peut être mis sur "1", index de MTG, jeu phare du projet
            string method = "GET";
            string url = "https://www.mkmapi.eu/ws/v1.1/output.json/expansion/" + idGame + "/" + expansionName;

            HttpWebRequest request = WebRequest.CreateHttp(url) as HttpWebRequest;
            OAuthHeader header = new OAuthHeader();
            request.Headers.Add(HttpRequestHeader.Authorization, header.getAuthorizationHeader(method, url));
            request.Method = method;

            HttpWebResponse response = request.GetResponse() as HttpWebResponse;
            string text = StreamToText(response);

            if (text == null) return null;
            JObject jObject = JObject.Parse(text);
            List<ProductMkm> products = new List<ProductMkm>();

            //dans magicmanager original, cette méthode faisait la comparaison du prix MKM avec le prix magasin du user.
            //Il était donc nécessaire d'appeler le stock pour vérifier si la carte existait, ainsi que son prix.
            //IStockRepository stockRepo = new StockRepository();
            //IEnumerable<Article> articles = stockRepo.GetStock();

            foreach (JObject prd in jObject["card"])
            {
                ProductMkm product = JsonConvert.DeserializeObject<ProductMkm>(prd.ToString().Replace("\r\n", ""), new MyFuckingJsonConverter());
                products.Add(product);
            }

           var collection = products.OrderBy(m => m.name.productName) as IEnumerable<ProductMkm>;

            foreach (ProductMkm prod in collection)
            {

                ProductRepo prRepo = new ProductRepo();
                ArticleRepo arRepo = new ArticleRepo();
                DailyPriceRepo dpRepo = new DailyPriceRepo();
                Article curAr = arRepo.FindBy(a => a.ProductId == prod.idProduct).FirstOrDefault();

                Product myProd = new Product();
                myProd.ProductId = prod.idProduct;
                myProd.ProductName = prod.name.productName;
                myProd.ProductUrl = prod.website;
                myProd.ImageUrl = prod.image;
                myProd.ExpansionId = prod.expIcon;
                //rarity not found, check parser
                //myProd.Rarity = prod.rarity;

                prRepo.Add(myProd);

                DailyPrice dp = new DailyPrice();
                dp.Average = prod.priceGuide.AVG;
                dp.Sell = prod.priceGuide.SELL;
                dp.Articleid = curAr.ArticleId;

                //if (isFoil) {low = LOWFOIL} else if (ex and better) {low = LOWEX+ else} else 
                if (curAr.isFoil){
                    dp.Low = prod.priceGuide.LOWFOIL;
                }
                else {
                    dp.Low = prod.priceGuide.LOW;
                }
                //countArticles nor LastEdited not found, check parser
                //dp.Count = prod.countArticles;
                //dp.LastEdited = prod.lastEdited;

                dpRepo.Add(dp);
            }

            return collection;
        }


        public ProductMkm ProductRequest(int id)
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
            string text = StreamToText(response);

            if (text == null) return null;
            ProductMkm prod = JsonConvert.DeserializeObject<ProductMkm>(text);
            
                Product myProd = new Product();
                myProd.ProductId = prod.idProduct;
                myProd.ProductName = prod.name.productName;
                myProd.ProductUrl = prod.website;
                myProd.ImageUrl = prod.image;
                myProd.ExpansionId = prod.expIcon;
                //rarity not found, check parser
                //myProd.Rarity = prod.rarity;

            ProductRepo prRepo = new ProductRepo();
            Product current = prRepo.FindBy(a => a.ProductId == myProd.ProductId).FirstOrDefault();
            if (current == null)
            {
                prRepo.Add(myProd);
            }
            return prod;
        }

        public IEnumerable<ArticleMkm> StockRequest()
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
            string text = StreamToText(response);

            if (text == null) return null;
            RootArticle root = JsonConvert.DeserializeObject<RootArticle>(text);
            return root.article as IEnumerable<ArticleMkm>;

        }

        //public CompositeType GetAccountUsingDataContract(CompositeType composite)
        //{
        //    throw new NotImplementedException();
        //}

        //public CompositeType GetArticleUsingDataContract(CompositeType composite)
        //{
        //    throw new NotImplementedException();
        //}

        //public CompositeType GetDataUsingDataContract(CompositeType composite)
        //{
        //    if (composite == null)
        //    {
        //        throw new ArgumentNullException("composite");
        //    }
        //    if (composite.BoolValue)
        //    {
        //        composite.StringValue += "Suffix";
        //    }
        //    return composite;
        //}

        //public CompositeType GetExpansionUsingDataContract(CompositeType composite)
        //{
        //    throw new NotImplementedException();
        //}

        //public CompositeType GetGameUsingDataContract(CompositeType composite)
        //{
        //    throw new NotImplementedException();
        //}

        //public CompositeType GetProductByExpansionUsingDataContract(CompositeType composite)
        //{
        //    throw new NotImplementedException();
        //}

        //public CompositeType GetProductUsingDataContract(CompositeType composite)
        //{
        //    throw new NotImplementedException();
        //}

        //public CompositeType GetStockUsingDataContract(CompositeType composite)
        //{
        //    throw new NotImplementedException();
        //}

        /// <summary>
        /// Class encapsulates tokens and secret to create OAuth signatures and return Authorization headers for web requests.
        /// </summary>
        class OAuthHeader
        {
            /// <summary>App Token</summary>
            /// located in webconfig, section AppSettings, currently hardcoded for testing
            /// 
            protected String appToken = WebConfigurationManager.AppSettings["appToken"];
            /// <summary>App Secret</summary>
            protected String appSecret = WebConfigurationManager.AppSettings["appSecret"];
            /// <summary>Access Token (Class should also implement an AccessToken property to set the value)</summary>
            protected String accessToken = WebConfigurationManager.AppSettings["accessToken"];
            /// <summary>Access Token Secret (Class should also implement an AccessToken property to set the value)</summary>
            protected String accessSecret = WebConfigurationManager.AppSettings["accessSecret"];
            /// <summary>OAuth Signature Method</summary>
            protected String signatureMethod = "HMAC-SHA1";
            /// <summary>OAuth Version</summary>
            protected String version = "1.0";
            /// <summary>All Header params compiled into a Dictionary</summary>
            protected IDictionary<String, String> headerParams;

            /// <summary>
            /// Constructor
            /// </summary>
            public OAuthHeader()
            {
                // String nonce = Guid.NewGuid().ToString("n");
                String nonce = "53eb1f44909d6";
                // String timestamp = (DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds.ToString();
                String timestamp = "1407917892";
                /// Initialize all class members
                this.headerParams = new Dictionary<String, String>();
                this.headerParams.Add("oauth_consumer_key", this.appToken);
                this.headerParams.Add("oauth_token", this.accessToken);
                this.headerParams.Add("oauth_nonce", nonce);
                this.headerParams.Add("oauth_timestamp", timestamp);
                this.headerParams.Add("oauth_signature_method", this.signatureMethod);
                this.headerParams.Add("oauth_version", this.version);
            }

            /// <summary>
            /// Pass request method and URI parameters to get the Authorization header value
            /// </summary>
            /// <param name="method">Request Method</param>
            /// <param name="url">Request URI</param>
            /// <returns>Authorization header value</returns>
            public String getAuthorizationHeader(String method, String url)
            {
                /// Add the realm parameter to the header params
                this.headerParams.Add("realm", url);

                /// Start composing the base string from the method and request URI
                String baseString = method.ToUpper()
                                  + "&"
                                  + Uri.EscapeDataString(url)
                                  + "&";

                /// Gather, encode, and sort the base string parameters
                SortedDictionary<String, String> encodedParams = new SortedDictionary<String, String>();
                foreach (KeyValuePair<String, String> parameter in this.headerParams)
                {
                    if (false == parameter.Key.Equals("realm"))
                    {
                        encodedParams.Add(Uri.EscapeDataString(parameter.Key), Uri.EscapeDataString(parameter.Value));
                    }
                }

                /// Expand the base string by the encoded parameter=value pairs
                List<String> paramStrings = new List<String>();
                foreach (KeyValuePair<String, String> parameter in encodedParams)
                {
                    paramStrings.Add(parameter.Key + "=" + parameter.Value);
                }
                String paramString = Uri.EscapeDataString(String.Join<String>("&", paramStrings));
                baseString += paramString;

                /// Create the OAuth signature
                String signatureKey = Uri.EscapeDataString(this.appSecret) + "&" + Uri.EscapeDataString(this.accessSecret);
                HMAC hasher = HMACSHA1.Create();
                hasher.Key = Encoding.UTF8.GetBytes(signatureKey);
                Byte[] rawSignature = hasher.ComputeHash(Encoding.UTF8.GetBytes(baseString));
                String oAuthSignature = Convert.ToBase64String(rawSignature);

                /// Include the OAuth signature parameter in the header parameters array
                this.headerParams.Add("oauth_signature", oAuthSignature);

                /// Construct the header string
                List<String> headerParamStrings = new List<String>();
                foreach (KeyValuePair<String, String> parameter in this.headerParams)
                {
                    headerParamStrings.Add(parameter.Key + "=\"" + parameter.Value + "\"");
                }
                String authHeader = "OAuth " + String.Join<String>(", ", headerParamStrings);

                return authHeader;
            }

        }
    }
}