using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagicManagerClassLibrary.dal
{
    class Article
    {
        public int ArticleId { get; set; }
        public int LanguageId { get; set; }
        public bool isFoil { get; set; }
        public bool isAltered { get; set; }
        public int isPlayset { get; set; }
        public bool isFirstEd { get; set; }
        public int SiteWideCount { get; set; }
        public DateTime WorkerEditTime { get; set; }
        public int ProductId { get; set; }
        public int DailyPriceId { get; set; }
    }
}
