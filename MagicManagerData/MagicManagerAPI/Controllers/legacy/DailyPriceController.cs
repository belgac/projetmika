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
    public class DailyPricesController : ApiController
    {
        // GET api/<controller>
        /// <summary>
        /// Retourne toutes la collection d'DailyPrices présent en db
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [Route("api/DailyPrice/get")]
        public IHttpActionResult Get()
        {
            var repo = new DailyPriceRepo();
            var dailyprices = repo.GetAll();//.ToList();
            //return new string[] { "value1", "value2" };
            if (dailyprices == null)
            {
                return NotFound();
            }
            return Ok(dailyprices);
        }

        [Route("api/DailyPrice/id/get")]
        public IHttpActionResult Get(int Productid)
        {
            var repo = new DailyPriceRepo();
            var dailyprice = repo.FindBy(a => a.Productid == Productid);
            if (dailyprice == null)
            {
                return NotFound();
            }
            return Ok(dailyprice);
        }

        [Route("api/DailyPrice/date/get")]
        public IHttpActionResult Get(DateTime date)
        {
            //attention : cette méthode demanderait d'avoir précisément la datetime du workeredit.
            //il faut donc formatter les dates de la bonne façon pour permettre une vraie recherche
            var repo = new DailyPriceRepo();
            var dailyprice = repo.FindBy(dp => dp.WorkerEditTime == date);
            if (dailyprice == null)
            {
                return NotFound();
            }
            return Ok(dailyprice);
        }


        [Route("api/DailyPrice/date/get")]
        public IHttpActionResult GetLast(int productId)
        {
            var repo = new DailyPriceRepo();
            var dailyprice = repo.FindBy(d => d.Productid == productId).OrderBy(d => d.WorkerEditTime).FirstOrDefault();
            if (dailyprice == null)
            {
                return NotFound();
            }
            return Ok(dailyprice);
        }

        



        // GET api/<controller>/5
        //public string Get(int id)
        //{
        //    return "value";
        //}

        // POST api/<controller>
        // [FromBody] : Utile que pour des page cshtml, ici on prend un objet db
        public void Post(DailyPrice DailyPrice)
        {
            var repo = new DailyPriceRepo();
            repo.Add(DailyPrice);
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
