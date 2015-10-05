using magicManager.Models.Game;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace magicManager.DAL
{
    interface IGameRepository : IDisposable
    {
        IEnumerable<Game> GetGame();
        Game GetGameByID(int Game);
        void InsertGale(Game game);
        void DeleteGame(int idGame);
        void UpdateGame(Game game);
        void Save();
    }
}
