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
    public class ExpansionsController : ApiController
    {
        // GET api/<controller>
        /// <summary>
        /// Retourne toutes la collection d'Expansions présent en db
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [Route("api/Expansion/get")]
        public IEnumerable<Expansion> Get()
        {
            var repo = new ExpansionRepo();
            return repo.GetAll();//.ToList();
            //return new string[] { "value1", "value2" };
        }

        [Route("api/Expansion/id/get")]
        public IQueryable<Expansion> Get(int id)
        {
            var repo = new ExpansionRepo();
            return repo.FindBy(a => a.ExpansionId == id);
        }

        // GET api/<controller>/5
        //public string Get(int id)
        //{
        //    return "value";
        //}

        // POST api/<controller>
        // [FromBody] : Utile que pour des page cshtml, ici on prend un objet db
        public void Post(Expansion Expansion)
        {
            var repo = new ExpansionRepo();
            repo.Add(Expansion);
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