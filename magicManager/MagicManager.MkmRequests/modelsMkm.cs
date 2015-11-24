using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MagicManager.MkmRequests
{


    public class ExpansionMkm
    {

        public int idExpansion { get; set; }

        public string name { get; set; }

        public int icon { get; set; }
    }


    public class GameMkm
    {

        public int idGame { get; set; }

        public string name { get; set; }
    }


    public class ArticleMkm
    {

        public int idArticle { get; set; }

        public int idProduct { get; set; }

        public LanguageMkm language { get; set; }

        public string comments { get; set; }

        public double price { get; set; }

        public int count { get; set; }

        public bool inShoppingCart { get; set; }

        public Seller seller { get; set; }

        public string condition { get; set; }

        public bool isFoil { get; set; }

        public bool isFirstEd { get; set; }

        public bool isSigned { get; set; }

        public bool isPlayset { get; set; }

        public bool isAltered { get; set; }

        public string rarity { get; set; }

        public DateTime lastEdited { get; set; }

    }


    public class LanguageMkm
    {

        public int idLanguage { get; set; }

        public string languageName { get; set; }
    }



    public class Lang
    {

        public int idLanguage { get; set; }

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


    public class ProductMkm : Card
    {

        public bool inStock { get; set; }

        public double myPrice { get; set; }

        public string rarity { get; set; }

        public int countArticles { get; set; }

        public int countFoils { get; set; }

        public PriceGuide priceGuide { get; set; }
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

        public string number { get; set; }
    }


    public class Name
    {

        // public Lang lang { get; set; }

        public long idLanguage { get; set; }

        public string languageName { get; set; }

        public string productName { get; set; }
    }


    public class RootArticle
    {
        public List<ArticleMkm> article { get; set; }
    }


    public class RootLanguage
    {
        public List<LanguageMkm> language { get; set; }
    }


    public class RootGame
    {
        public List<GameMkm> game { get; set; }
    }


    public class RootExpansion
    {
        public List<ExpansionMkm> expansion { get; set; }
    }


    public class RootProduct
    {
        public List<ProductMkm> product { get; set; }
    }

    public class Seller
    {
        public int idUser { get; set; }
        public string username { get; set; }
        public string country { get; set; }
        public int isCommercial { get; set; }
        public int riskGroup { get; set; }
        public int reputation { get; set; }
        public int shipsFast { get; set; }
        public int sellCount { get; set; }
        public bool onVacation { get; set; }
        public int idDisplayLanguage { get; set; }
    }
}