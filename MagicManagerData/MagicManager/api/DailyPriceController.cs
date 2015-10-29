using MagicManager.dal.Repositories;
using MagicManager.Models;
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
        public IEnumerable<DailyPrice> Get()
        {
            var repo = new DailyPriceRepo();
            return repo.GetAll();//.ToList();
            //return new string[] { "value1", "value2" };
        }

        [Route("api/DailyPrice/id/get")]
        public IQueryable<DailyPrice> Get(int id)
        {
            var repo = new DailyPriceRepo();
            return repo.FindBy(a => a.DailyPriceId == id);
        }

        [Route("api/DailyPrice/date/get")]
        public IQueryable<DailyPrice> Get(DateTime date)
        {
            var repo = new DailyPriceRepo();
            return repo.FindBy(dp => dp.LastEdited == date);
        }

        // ESSAI DE METHODE POUR RECUPERER LES DailyPriceS PAR PRIX.
        // PEUT ETRE INUTILE
        //[Route("api/DailyPrice/dailyPrice/get")]
        //public IQueryable<DailyPrice> Get(int id)
        //{
        //    var repo = new DailyPriceRepo();
        //    return repo.FindBy(a => a.DailyPrices.FirstOrDefault() == id);
        //}

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
