using MagicManager.dal.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MagicManager.Model;

namespace MagicManagerAPI.Controllers
{
    public class ProductsController : ApiController
    {
       
        /// GET api/<controler>
        /// <summary>
        /// Retourne toute la collection de produits présent en Db
        /// </summary>
        /// <returns></returns>
        [System.Web.Http.Authorize]
        [System.Web.Http.Route("api/product/get")]
        public IHttpActionResult Get()
        {
            var repo = new ProductRepo();
            var products = repo.GetAll();//.ToList();
            //return new string[] { "value1", "value2" };
            if (products == null)
            {
                return NotFound();
            }
            return Ok(products);

        }

        public IHttpActionResult Get(int id)
        {
            var repo = new ProductRepo();
            Product prod = repo.FindBy(p => p.ProductId == id).FirstOrDefault();
            Console.WriteLine("success");
            if (prod == null)
            {
                return NotFound();
            }

            return Ok(prod);
        }

        [Route("api/product/{expansionId}/get")]
        public IHttpActionResult GetInExp(int expansionId)
        {
            var repo = new ProductRepo();
            var prodInExp = repo.FindBy(p => p.ExpansionId == expansionId);

            if (prodInExp == null)
            {
                return NotFound();
            }
            return Ok(prodInExp);
        }

        [System.Web.Http.Route("api/product/{userInput}/get")]
        public IHttpActionResult Get(string userInput)
        {
            var repo = new ProductRepo();
            var prodName = repo.FindBy(p => p.ProductName == (userInput).ToString());

            if (prodName == null)
            {
                return NotFound();
            }
            return Ok(prodName);
        }

        [System.Web.Http.Route("api/product/workeredittime/get")]
        public IHttpActionResult Get(DateTime date)
        {
            var repo = new ProductRepo();
            var prodWorkerDate = repo.FindBy(p => p.WorkerEditTime == date);

            if (prodWorkerDate == null)
            {
                return NotFound();
            }
            return Ok(prodWorkerDate);

        }

    }
}
