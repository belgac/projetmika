using MagicManager.dal.Repositories;
using MagicManager.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MagicManager
{
    public class KvtmController : ApiController
    {

        // GET api/<controller>
        /// <summary>
        /// Retourne toutes la collection d'articles présent en db
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [Route("api/kvtm/get")]
        public IHttpActionResult Get(string title)
        {
            var repo = new KvtmRepo();
            var kvtm = repo.FindBy(k => k.Title == title).FirstOrDefault();
            if (kvtm == null)
            {
                return NotFound();
            }
            return Ok(kvtm);
        }
    }
}
