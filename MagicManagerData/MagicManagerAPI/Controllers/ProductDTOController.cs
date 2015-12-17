using MagicManager.dal.Repositories;
using MagicManager.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MagicManagerAPI.Controllers
{
    public class ProductDTOController : ApiController
    {
        public IHttpActionResult Get(int id)
        {
            var prRepo = new ProductRepo();
            var dpRepo = new DailyPriceRepo();
            var arRepo = new ArticleRepo();
            var prod = prRepo.FindBy(p => p.ProductId == id).FirstOrDefault();
            var dp = dpRepo.FindBy(d => d.Productid == id);
            var art = arRepo.FindBy(a => a.ProductId == id).FirstOrDefault();

            ProductDTO prdDto = new ProductDTO();
            prdDto.ProductId = id;
            if (prod != null)
            {
                prdDto.ProductName = prod.ProductName;
                prdDto.ProductUrl = prod.ProductUrl;
                prdDto.ImageUrl = prod.ImageUrl;
                prdDto.Rarity = prod.Rarity;
                prdDto.ExpansionId = prod.ExpansionId;
            }
            else
            {
                prdDto.ProductName = "Not Found";
                prdDto.ProductUrl = "";
                prdDto.ImageUrl = "default.jpg";
                prdDto.Rarity = "So rare even we couldn't find it";
                prdDto.ExpansionId = 1;
            }

            List<DailyPrice> lastDp = new List<DailyPrice>();
            if (dp != null)
            {
                lastDp = dp.OrderBy(d => d.WorkerEditTime).ToList();
                prdDto.lastDp = lastDp;
            }
            //todo : implement default dp list
            else
            {
                prdDto.lastDp = lastDp;
            }

            if (art != null)
            {
                prdDto.isAltered = art.isAltered;
                prdDto.isFirstEd = art.isFirstEd;
                prdDto.isFoil = art.isFoil;
                prdDto.isPlayset = art.isPlayset;
                prdDto.isSigned = art.isSigned;
                prdDto.Count = art.Count;
             //prdDto.Lang = art.Lang.Name;
            }
            else
            {
                prdDto.isAltered = false;
                prdDto.isFirstEd = false;
                prdDto.isFoil = false;
                prdDto.isPlayset = false;
                prdDto.isSigned = false;
                prdDto.Count = 0;
               //prdDto.Lang = "English";
            }
                

            if (prdDto == null)
            {
                return NotFound();
            }
            return Ok(prdDto);
        }

    }
}
