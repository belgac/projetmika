using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagicManagerClassLibrary.dal
{
    class WorkerAction
    {
        public int WorkerActionId { get; set; }
        public string Type { get; set; }
        public string Comment { get; set; }
        public DateTime Date { get; set; }

    }
}
