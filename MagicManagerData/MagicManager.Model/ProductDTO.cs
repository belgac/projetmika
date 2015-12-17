using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagicManager.Model
{
    public class ProductDTO
    {
        //From product : 
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string ProductUrl { get; set; }
        public string ImageUrl { get; set; }
        public string Rarity { get; set; }
        public int ExpansionId { get; set; }
        //From DailyPrice : 
        public List<DailyPrice> lastDp { get; set; }
        //From Article
        public int? Count { get; set; }
        public bool isFoil { get; set; }
        public bool isSigned { get; set; }
        public bool isAltered { get; set; }
        public bool isPlayset { get; set; }
        public bool isFirstEd { get; set; }
    }
}
