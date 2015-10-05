using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using magicManager.Models.Articles;
using magicManager.Models;
using Newtonsoft.Json;

namespace magicManager.DAL
{
    public class ArticleRepository : IArticleRepository
    {
        RequestHelper essai = new RequestHelper();

        public void DeleteArticle(int idArticle)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Article> GetArticles()
        {
            string text = essai.ArticleRequest();
            if (text == null) return null;
            RootArticle root = JsonConvert.DeserializeObject<RootArticle>(text);
            return root.article as IEnumerable<Article>;
        }


        public Article GetArticlesById(int idArticle)
        {
            throw new NotImplementedException();
        }

        public void InsertArticle(Article article)
        {
            throw new NotImplementedException();
        }

        public void Save()
        {
            throw new NotImplementedException();
        }

        public void UpdateArticle(Article article)
        {
            throw new NotImplementedException();
        }


        public void Dispose()
        {
            throw new NotImplementedException();
        }


    }
}