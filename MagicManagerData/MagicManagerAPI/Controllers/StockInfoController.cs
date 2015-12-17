using MagicManager.dal.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MagicManagerAPI.Controllers
{
    public class stockInfoController : ApiController
    {
        public IHttpActionResult Get(int id)
        {
            StockInfoRepo repo = new StockInfoRepo();
            var stockInfo = repo.FindBy(s => s.StockInfoId == id).OrderBy(s => s.WorkerEditTime).FirstOrDefault();

            if (stockInfo == null)
            {
                return NotFound();
            }
            return Ok(stockInfo);
        }
    }
}
