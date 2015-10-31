using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagicManagerClassLibrary.dal
{
    class Expansion
    {
        public int ExpansionId { get; set; }
        public string Name { get; set; }
        public int Icon { get; set; }
        public DateTime WorkerEditTime { get; set; }
        public int GameId { get; set; }
        public List<Product> Products { get; set; }
    }
}
