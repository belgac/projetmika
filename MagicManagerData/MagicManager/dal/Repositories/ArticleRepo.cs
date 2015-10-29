using MagicManager.dal.Interfaces;
using MagicManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MagicManager.dal.Repositories
{
    public class ArticleRepo : GenericRepository<MagicManagerDataEntities, Article>, IArticles
    {
        public bool check(int id)
        {
            var q = FindBy(a => a.ArticleId == id);
            return q.Any();
        }

    }
}