using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace magicManager.Models.Game
{
    public class Game
    {
        public int idGame { get; set; }
        public string name { get; set; }
    }

    public class RootGame
    {
        public List<Game> game { get; set; }
    }
}