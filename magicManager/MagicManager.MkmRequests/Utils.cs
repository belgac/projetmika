using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MagicManager.MkmRequests
{
    class Utils
    {
        public static string StreamToText(HttpWebResponse response)
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
    }
}
