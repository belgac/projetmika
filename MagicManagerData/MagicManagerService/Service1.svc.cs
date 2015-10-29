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

namespace MagicManagerService
{
    // REMARQUE : vous pouvez utiliser la commande Renommer du menu Refactoriser pour changer le nom de classe "Service1" dans le code, le fichier svc et le fichier de configuration.
    // REMARQUE : pour lancer le client test WCF afin de tester ce service, sélectionnez Service1.svc ou Service1.svc.cs dans l'Explorateur de solutions et démarrez le débogage.
    public class Service1 : IService1
    {

        private string Convertor(string requestResponse)
        {
            //TODO : insérer la logique du convertisseur pour transformer l'article MKM en article de notre db
            // A FAIRE POUR CHAQUE ENTITE ou trouver méthode pour tous

            //a supprimer, sert uniquement à compiler sans erreur
            return requestResponse;

        }

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

        public string ArticleRequest(int id)
        {
            //retourne l'article spécifié via IdArticle
            string method = "GET";
            string url = "https://www.mkmapi.eu/ws/v1.1/output.json/articles/" + id;

            HttpWebRequest request = WebRequest.CreateHttp(url) as HttpWebRequest;
            OAuthHeader header = new OAuthHeader();
            request.Headers.Add(HttpRequestHeader.Authorization, header.getAuthorizationHeader(method, url));
            request.Method = method;

            HttpWebResponse response = request.GetResponse() as HttpWebResponse;
            return StreamToText(response);

            //TODO: add check method to see if returned items already exist in tables, otherwise add them
        }

        public string ExpansionRequest(int id)
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
            return StreamToText(response);

            //TODO: add check method to see if returned items already exist in tables, otherwise add them
        }

        public string GameRequest()
        {
            //retourne la liste des jeux au format Json
            string method = "GET";
            string url = "https://www.mkmapi.eu/ws/v1.1/output.json/games";

            HttpWebRequest request = WebRequest.CreateHttp(url) as HttpWebRequest;
            OAuthHeader header = new OAuthHeader();
            request.Headers.Add(HttpRequestHeader.Authorization, header.getAuthorizationHeader(method, url));
            request.Method = method;

            HttpWebResponse response = request.GetResponse() as HttpWebResponse;
            return StreamToText(response);
        }

        //public CompositeType GetAccountUsingDataContract(CompositeType composite)
        //{
        //    throw new NotImplementedException();
        //}

        //public CompositeType GetArticleUsingDataContract(CompositeType composite)
        //{
        //    throw new NotImplementedException();
        //}

        public string GetData(int value)
        {
            var repo = new ArticleRepo();

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

        public string ProductByExpansionRequest(int idGame, string expansionName)
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
            return StreamToText(response);
        }

        public string ProdutRequest(int id)
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
            // XmlDocument doc = new XmlDocument();
            // doc.Load(response.GetResponseStream());
            // proceed further

            return StreamToText(response);

            //TODO: add check method to see if returned items already exist in tables, otherwise add them
        }

        public string StockRequest()
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
            return StreamToText(response);

            //TODO: add check method to see if returned items already exist in tables, otherwise add them
        }

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
