using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using magicManager.Models.Articles;

namespace magicManager.DAL
{
    interface IArticleRepository : IDisposable
    {
        IEnumerable<Article> GetArticles();
        Article GetArticlesById(int idArticle);
        void InsertArticle(Article article);
        void DeleteArticle(int idArticle);
        void UpdateArticle(Article article);
        void Save();
    }
}
