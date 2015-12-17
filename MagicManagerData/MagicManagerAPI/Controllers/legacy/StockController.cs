using MagicManager.dal.Repositories;
using MagicManager.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MagicManager.api
{
    public class StockController : ApiController
    {

        // GET api/<controller>
        /// <summary>
        /// Retourne toutes la collection de Stock présent en db
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [Route("api/StockValue/get")]
        public IHttpActionResult Get()
        {
            var repo = new StockInfoRepo();
            var stockvalue = repo.GetAll();
            if (stockvalue == null)
            {
                return NotFound();
            }
            return Ok(stockvalue);
        }


    }
}
