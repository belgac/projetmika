using MagicManager.dal.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MagicManagerAPI.Controllers
{
    public class MarketInfoController : ApiController
    {
        public IHttpActionResult Get(int id)
        {
            MarketInfoRepo mktInfRepo = new MarketInfoRepo();
            var marketInfo = mktInfRepo.FindBy(m => m.MarketInfoId == id).FirstOrDefault();

            if (marketInfo == null)
            {
                return NotFound();
            }
            return Ok(marketInfo);
        }
        
    }
}
