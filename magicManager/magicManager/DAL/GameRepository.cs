using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using magicManager.Models.Game;
using magicManager.Models;
using Newtonsoft.Json;

namespace magicManager.DAL
{
    public class GameRepository : IGameRepository
    {
        RequestHelper essai = new RequestHelper();

        public void DeleteGame(int idGame)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Game> GetGame()
        {
            string result = essai.GameRequest();
            if (result == null) return null;
            RootGame root = JsonConvert.DeserializeObject<RootGame>(result);
            return root.game as IEnumerable<Game>;
        }

        public Game GetGameByID(int Game)
        {
            throw new NotImplementedException();
        }

        public void InsertGale(Game game)
        {
            throw new NotImplementedException();
        }

        public void Save()
        {
            throw new NotImplementedException();
        }

        public void UpdateGame(Game game)
        {
            throw new NotImplementedException();
        }
    }
}