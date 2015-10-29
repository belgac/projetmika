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
    public class ProductController : ApiController
    {
        /// GET api/<controler>
        /// <summary>
        /// Retourne toute la collection de produits présent en Db
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [Route("api/product/get")]
        public IEnumerable<Product> Get()
        {
            var repo = new ProductRepo();
            return repo.GetAll();//.ToList();
            //return new string[] { "value1", "value2" };
        }

        [Route("api/product/id/get")]
        public IQueryable<Product> Get(int id)
        {
            var repo = new ProductRepo();
            return repo.FindBy(p => p.ProductId == id);
        }

        [Route("api/product/name/get")]
        public IQueryable<Product> Get(string userInput)
        {
            var repo = new ProductRepo();
            return repo.FindBy(p => p.ProductName == (userInput).ToString());
        }

        [Route("api/product/workeredittime/get")]
        public IQueryable<Product> Get(DateTime date)
        {
            var repo = new ProductRepo();
            return repo.FindBy(p => p.WorkerEditTime == date);
        }


        // GET: Product/Details/5
        //public ActionResult Details(int id)
        //{
        //    return View();
        //}

        // GET: Product/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Product/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Product/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Product/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Product/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Product/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
