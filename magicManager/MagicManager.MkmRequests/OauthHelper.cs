using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace MagicManager.MkmRequests
{
 
        /// <summary>
        /// Class encapsulates tokens and secret to create OAuth signatures and return Authorization headers for web requests.
        /// </summary>
        class OAuthHeader
        {
        /// <summary>App Token</summary>
        /// located in webconfig, section AppSettings, currently hardcoded for testing
        /// 
        //protected String appToken = WebConfigurationManager.AppSettings["appToken"];
        protected String appToken = "jFtC5f7xi0YSWCTe";
            /// <summary>App Secret</summary>
            protected String appSecret = "ygxYSeYjWvo5JqxMa3kJ7PGEyq8gpAAg";
            /// <summary>Access Token (Class should also implement an AccessToken property to set the value)</summary>
            protected String accessToken = "BMpwtb71CUJ9cIVZREtm2c9GdkBNgH7q";
            /// <summary>Access Token Secret (Class should also implement an AccessToken property to set the value)</summary>
            protected String accessSecret = "W9S7HwknbffTgTWtzekkp2MtcaSwpKF8";
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
