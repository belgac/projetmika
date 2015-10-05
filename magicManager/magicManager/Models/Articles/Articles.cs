using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace magicManager.Models.Articles
{
        public class Language
        {
            public int idLanguage { get; set; }
            public string languageName { get; set; }
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

        public class Article
        {
            public int idArticle { get; set; }
            public int idProduct { get; set; }
            public Language language { get; set; }
            public string comments { get; set; }
            public double price { get; set; }
            public int count { get; set; }
            public bool inShoppingCart { get; set; }
            public Seller seller { get; set; }
            public string condition { get; set; }
            public bool isFoil { get; set; }
            public bool isSigned { get; set; }
            public bool isPlayset { get; set; }
            public bool isAltered { get; set; }
        }

        public class RootArticle
        {
            public List<Article> article { get; set; }
        }

}