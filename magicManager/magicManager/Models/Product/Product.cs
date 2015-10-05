using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using magicManager.Models.Expansions;

namespace magicManager.Models
{
    public class Lang
    {
        public int idLanguage { get; set; }
        public string languageName { get; set; }
        public string productName { get; set; }
    }

    public class Name
    {
        // public Lang lang { get; set; }
        public long idLanguage { get; set; }
        public string languageName { get; set; }
        public string productName { get; set; }
    }

    public class Category
    {
        public long idCategory { get; set; }
        public string categoryName { get; set; }
    }

    public class PriceGuide
    {
        public double SELL { get; set; }
        public double LOW { get; set; }
        public double LOWEX { get; set; }
        public double LOWFOIL { get; set; }
        public double AVG { get; set; }
        public double TREND { get; set; }
    }

    public class Product : Card
    {
        public PriceGuide priceGuide { get; set; }
        public bool inStock { get; set; }
        public double myPrice { get; set; }
    }

    public class RootProduct
    {
        public Expansion expansion { get; set; }
        public Product product { get; set; }
    }

  


    public class RootCard
    {
       public List<Product> card { get; set; }
    }

    

    public class Card
    {
        public int idProduct { get; set; }
        public int idMetaproduct { get; set; }
        public int idGame { get; set; }
        public string countReprints { get; set; }
        public Name name { get; set; }
        public string website { get; set; }
        public string image { get; set; }
        public Category category { get; set; }
        public string expansion { get; set; }
        public int expIcon { get; set; }
        public object number { get; set; }
    }


    

}