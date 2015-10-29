using MagicManager.dal.Repositories;
using MagicManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

/// <summary>
/// Class api Angular - localdb (Sql en local)
/// </summary>
namespace MagicManager
{
    public class ArticlesController : ApiController
    {
        // GET api/<controller>
        /// <summary>
        /// Retourne toutes la collection d'articles présent en db
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [Route("api/article/get")]
        public IEnumerable<Article> Get()
        {
            var repo = new ArticleRepo();
            return repo.GetAll();//.ToList();
            //return new string[] { "value1", "value2" };
        }

        [Route("api/article/id/get")]
        public IQueryable<Article> Get(int id)
        {
            var repo = new ArticleRepo();
            return repo.FindBy(a => a.ArticleId == id);
        }


        // ESSAI DE METHODE POUR RECUPERER LES ARTICLES PAR PRIX.
        // PEUT ETRE INUTILE
        //[Route("api/article/dailyPrice/get")]
        //public IQueryable<Article> Get(int id)
        //{
        //    var repo = new ArticleRepo();
        //    return repo.FindBy(a => a.DailyPrices.FirstOrDefault() == id);
        //}

        // GET api/<controller>/5
        //public string Get(int id)
        //{
        //    return "value";
        //}

        // POST api/<controller>
        // [FromBody] : Utile que pour des page cshtml, ici on prend un objet db
        public void Post(Article article)
        {
            var repo = new ArticleRepo();
            repo.Add(article);
            //On sauve le context, pour forcer la màj db
            repo.Save();
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
            throw new NotImplementedException();
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}