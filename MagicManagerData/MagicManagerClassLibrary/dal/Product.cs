using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagicManagerClassLibrary.dal
{
    class Product
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string ProductUrl { get; set; }
        public string ImageUrl { get; set; }
        public string Rarity { get; set; }
        public DateTime WorkerEditTime { get; set; }
        public int ExpansionId { get; set; }
        public int ArticleId { get; set; }
    }
}
