using MagicManager.dal.Repositories;
using MagicManager.Model;
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
        public IHttpActionResult Get()
        {
            var repo = new ArticleRepo();
            var articles = repo.GetAll();//.ToList();
            //return new string[] { "value1", "value2" };
            if (articles == null)
            {
                return NotFound();
            }
            return Ok(articles);
        }

        [Route("api/article/id/get")]
        public IHttpActionResult Get(int id)
        {
            var repo = new ArticleRepo();
            var article = repo.FindBy(a => a.ArticleId == id);
            if (article == null)
            {
                return NotFound();
            }
            return Ok(article);
        }

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