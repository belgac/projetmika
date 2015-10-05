using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Configuration;
using System.Xml;

namespace magicManager.Models
{
    public class RequestHelper
    {
        private string StreamToText(HttpWebResponse response)
        {
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

            //TODO : remplacer URL par une variable paramétrée en fonction de la demande de l'USER
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

        public string GameRequest()
        {
            string method = "GET";
            string url = "https://www.mkmapi.eu/ws/v1.1/output.json/games";

            HttpWebRequest request = WebRequest.CreateHttp(url) as HttpWebRequest;
            OAuthHeader header = new OAuthHeader();
            request.Headers.Add(HttpRequestHeader.Authorization, header.getAuthorizationHeader(method, url));
            request.Method = method;

            HttpWebResponse response = request.GetResponse() as HttpWebResponse;
            return StreamToText(response);

        }

        public string ArticleRequest()
        {
            string method = "GET";
            string url = "https://www.mkmapi.eu/ws/v1.1/output.json/articles/1";

            HttpWebRequest request = WebRequest.CreateHttp(url) as HttpWebRequest;
            OAuthHeader header = new OAuthHeader();
            request.Headers.Add(HttpRequestHeader.Authorization, header.getAuthorizationHeader(method, url));
            request.Method = method;

            HttpWebResponse response = request.GetResponse() as HttpWebResponse;
            return StreamToText(response);

        }

        public string ProductInExpansionRequest(int idGame, string expansionName)
        {
            string method = "GET";
            string url = "https://www.mkmapi.eu/ws/v1.1/output.json/expansion/"+ idGame+ "/" + expansionName;

            HttpWebRequest request = WebRequest.CreateHttp(url) as HttpWebRequest;
            OAuthHeader header = new OAuthHeader();
            request.Headers.Add(HttpRequestHeader.Authorization, header.getAuthorizationHeader(method, url));
            request.Method = method;

            HttpWebResponse response = request.GetResponse() as HttpWebResponse;
            return StreamToText(response);
        }


        public string ProductRequest(int id)
        {

            //TODO : remplacer URL par une variable paramétrée en fonction de la demande de l'USER
            string method = "GET";
            string url = "https://www.mkmapi.eu/ws/v1.1/output.json/product/"+id;

            HttpWebRequest request = WebRequest.CreateHttp(url) as HttpWebRequest;
            OAuthHeader header = new OAuthHeader();
            request.Headers.Add(HttpRequestHeader.Authorization, header.getAuthorizationHeader(method, url));
            request.Method = method;

            //TODO : parser le JSON en remplaçant les suites "1", "2", "3", "4" et "5" par "lang" -.-'

            HttpWebResponse response = request.GetResponse() as HttpWebResponse;
            // XmlDocument doc = new XmlDocument();
            // doc.Load(response.GetResponseStream());
            // proceed further

            return StreamToText(response);
        }

        public string ExpansionRequest()
        {

            //TODO : remplacer URL par une variable paramétrée en fonction de la demande de l'USER
            string method = "GET";
            string url = "https://www.mkmapi.eu/ws/v1.1/output.json/expansion/1";

            HttpWebRequest request = WebRequest.CreateHttp(url) as HttpWebRequest;
            OAuthHeader header = new OAuthHeader();
            request.Headers.Add(HttpRequestHeader.Authorization, header.getAuthorizationHeader(method, url));
            request.Method = method;

            HttpWebResponse response = request.GetResponse() as HttpWebResponse;
            return StreamToText(response);
        }

        public string ExpansionByIdRequest(int idExpansion)
        {
            string method = "GET";
            string url = "https://www.mkmapi.eu/ws/v1.1/output.json/expansion/" + idExpansion;

            HttpWebRequest request = WebRequest.CreateHttp(url) as HttpWebRequest;
            OAuthHeader header = new OAuthHeader();
            request.Headers.Add(HttpRequestHeader.Authorization, header.getAuthorizationHeader(method, url));
            request.Method = method;

            HttpWebResponse response = request.GetResponse() as HttpWebResponse;
            return StreamToText(response);
        }

        public string StockRequest()
        {

            //TODO : remplacer URL par une variable paramétrée en fonction de la demande de l'USER
            string method = "GET";
            string url = "https://www.mkmapi.eu/ws/v1.1/output.json/stock";

            HttpWebRequest request = WebRequest.CreateHttp(url) as HttpWebRequest;
            OAuthHeader header = new OAuthHeader();
            request.Headers.Add(HttpRequestHeader.Authorization, header.getAuthorizationHeader(method, url));
            request.Method = method;

            HttpWebResponse response = request.GetResponse() as HttpWebResponse;
            return StreamToText(response);
        }

    }


    /// <summary>
    /// Class encapsulates tokens and secret to create OAuth signatures and return Authorization headers for web requests.
    /// </summary>
    class OAuthHeader
    {
        /// <summary>App Token</summary>
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