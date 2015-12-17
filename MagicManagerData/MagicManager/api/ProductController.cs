using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Mvc;
using System.Web;
using System.Web.Http;
using MagicManager.Model;
using MagicManager.dal.Repositories;

namespace MagicManager.api
{
    public class ProductController : ApiController
    {
        /// GET api/<controler>
        /// <summary>
        /// Retourne toute la collection de produits présent en Db
        /// </summary>
        /// <returns></returns>
        [System.Web.Http.Authorize]
        [System.Web.Http.Route("api/product/get")]
        public IEnumerable<Product> Get()
        {
            var repo = new ProductRepo();
            return repo.GetAll();//.ToList();
            //return new string[] { "value1", "value2" };
        }

        [System.Web.Http.Route("api/product/id/get")]
        public IHttpActionResult Get(int id)
        {
            var repo = new ProductRepo();
            IQueryable<Product> prod = repo.FindBy(p => p.ProductId == id);

            if (prod == null)
            {
                return NotFound();
            }

            return Ok(prod);
        }

        [System.Web.Http.Route("api/product/name/get")]
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


        // GET: Product/Details/5
        //public ActionResult Details(int id)
        //{
        //    return View();
        //}

        // GET: Product/Create
        //public ActionResult Create()
        //{
        //    return View();
        //}

        // POST: Product/Create
        //[HttpPost]
        //public ActionResult Create(FormCollection collection)
        //{
        //    try
        //    {
        //        // TODO: Add insert logic here

        //        return RedirectToAction("Index");
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        //// GET: Product/Edit/5
        //public ActionResult Edit(int id)
        //{
        //    return View();
        //}

        //// POST: Product/Edit/5
        //[HttpPost]
        //public ActionResult Edit(int id, FormCollection collection)
        //{
        //    try
        //    {
        //        // TODO: Add update logic here

        //        return RedirectToAction("Index");
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        //// GET: Product/Delete/5
        //public ActionResult Delete(int id)
        //{
        //    return View();
        //}

        //// POST: Product/Delete/5
        //[HttpPost]
        //public ActionResult Delete(int id, FormCollection collection)
        //{
        //    try
        //    {
        //        // TODO: Add delete logic here

        //        return RedirectToAction("Index");
        //    }
        //    catch
        //    {
        //        return View();
        //    }
       // }
    }
}
