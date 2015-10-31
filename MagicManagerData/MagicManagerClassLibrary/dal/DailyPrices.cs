using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagicManagerClassLibrary.dal
{
    class DailyPrices
    {
        public int DailyPriceId { get; set; }
        public int Sell { get; set; }
        public int Low { get; set; }
        public int Average { get; set; }
        public int Count { get; set; }
        public int Price { get; set; }
        public DateTime LastEdited { get; set; }
        public DateTime WorkerEditTime { get; set; }
        public int ArticleId { get; set; }

    }
}
