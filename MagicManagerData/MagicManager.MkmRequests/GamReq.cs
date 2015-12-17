using MagicManager.dal.Repositories;
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
    public class GamReq
    {

        public static IEnumerable<GameMkm> GameRequest()
        {
            //retourne la liste des jeux au format Json
            string method = "GET";
            string url = "https://www.mkmapi.eu/ws/v1.1/output.json/games";

            HttpWebRequest request = WebRequest.CreateHttp(url) as HttpWebRequest;
            OAuthHeader header = new OAuthHeader();
            request.Headers.Add(HttpRequestHeader.Authorization, header.getAuthorizationHeader(method, url));
            request.Method = method;

            HttpWebResponse response = request.GetResponse() as HttpWebResponse;
            string text = Utils.StreamToText(response);

            if (text == null) return null;
            RootGame root = JsonConvert.DeserializeObject<RootGame>(text);
            var collection = root.game as IEnumerable<GameMkm>;

            GameRepo gaRepo = new GameRepo();

            foreach (GameMkm game in collection)
            {

                Game curGame = gaRepo.FindBy(g => g.GameId == game.idGame).FirstOrDefault();

                if (curGame == null)
                {
                    curGame = new Game();
                    curGame.GameId = game.idGame;
                    curGame.WorkerEditTime = DateTime.Now;
                    curGame.Name = game.name;

                    //myGame.Expansion = ExpReq.ExpansionRequest(game.idGame) as ICollection<Expansion>;

                    gaRepo.Add(curGame);
                    Console.WriteLine("game added!");
                }
                else
                {
                    curGame.Name = game.name;
                    curGame.WorkerEditTime = DateTime.Now;
                }
                gaRepo.Save();
            }
            return collection;
        }

    }
}
