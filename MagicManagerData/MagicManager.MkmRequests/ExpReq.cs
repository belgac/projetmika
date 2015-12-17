using MagicManager.dal.Repositories;
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
    public class ExpReq
    {
        public static IEnumerable<Expansion> ExpansionRequest(int idGame)
        {

            //retourne l'expansion spécifié par l'id.
            //Utile pour dropdowns lists du projet beta, sera certainement utile pour la configuration user
            string method = "GET";
            string url = "https://www.mkmapi.eu/ws/v1.1/output.json/expansion/" + idGame;

            HttpWebRequest request = WebRequest.CreateHttp(url) as HttpWebRequest;
            OAuthHeader header = new OAuthHeader();
            request.Headers.Add(HttpRequestHeader.Authorization, header.getAuthorizationHeader(method, url));
            request.Method = method;

            HttpWebResponse response = request.GetResponse() as HttpWebResponse;
            string text = Utils.StreamToText(response);

            if (text == null) return null;

            ExpansionRepo eRepo = new ExpansionRepo();

            JObject jObject = JObject.Parse(text);
            foreach (JObject exp in jObject["expansion"])
            {
                ExpansionMkm expansion = JsonConvert.DeserializeObject<ExpansionMkm>(exp.ToString());
                Expansion curExp = eRepo.FindBy(e => e.ExpansionId == expansion.idExpansion).FirstOrDefault();
                if (curExp == null)
                {
                    curExp = new Expansion();
                    curExp.ExpansionId = expansion.idExpansion;
                    curExp.GameId = idGame;
                    curExp.Icon = expansion.icon;
                    curExp.Name = expansion.name;
                    curExp.WorkerEditTime = DateTime.Now;
                    eRepo.Add(curExp);
                }
                else
                {
                    curExp.Icon = expansion.icon;
                    curExp.Name = expansion.name;
                    curExp.WorkerEditTime = DateTime.Now;
                }
                eRepo.Save();
            }
            return eRepo.GetAll().ToList();
        }
    }
}
