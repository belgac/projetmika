using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagicManagerClassLibrary
{
    public class Game
    {
        public int GameId { get; set; }
        public string Name { get; set; }
        public DateTime WorkerEditTime { get; set; }
        public List<Expansion> Expansions { get; set; }
    }
}
