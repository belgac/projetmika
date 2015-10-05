using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace magicManager.Models.Expansions
{
        public class Expansion
        {
            public int idExpansion { get; set; }
            public string name { get; set; }
            public int icon { get; set; }
        }

        public class RootExpansion
        {
            public List<Expansion> expansion { get; set; }
        }

}